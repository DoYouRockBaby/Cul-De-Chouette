using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using Core;
using Core.ThrowingTrigger;
using Core.Action;

namespace Server
{
    public delegate void OnMessageHandler(string message);
    public class NetworkServer : Output
    {
        public Hashtable Clients = new Hashtable();
        private int ClientsCount = 0;
        public GameManager GameManager = new GameManager();

        public NetworkServer()
        {
            Rules rules = new Rules();

            AbstractTrigger chouette = new ChouetteTrigger();
            AbstractTrigger chouetteVelute = new ChouetteVeluteTrigger();
            AbstractTrigger culDeChouette = new CulDeChouetteTrigger();
            AbstractTrigger suite = new SuiteTrigger();
            AbstractTrigger velute = new VeluteTrigger();
            rules.ThrowingTriggers = new AbstractTrigger[] { chouette, chouetteVelute, culDeChouette, suite, velute };

            AbstractAction grelotteCaPicote = new GrelotteCaPicote();
            AbstractAction pasMouLeCaillou = new PasMouLeCaillouAction();
            AbstractAction throwCubes = new ThrowCubesAction();
            rules.Actions = new AbstractAction[] { grelotteCaPicote, pasMouLeCaillou, throwCubes };

            GameManager.Rules = rules;
            GameManager.Output = this;
        }
        public void Start(int port)
        {
            TcpListener serverSocket = new TcpListener(port);
            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();
            Console.WriteLine ("Server Started ....");
            while ((true))
            {
                string name = "Joueur " + (++ClientsCount);

                clientSocket = serverSocket.AcceptTcpClient();
                Clients.Add(name, clientSocket);

                Player player = new Player();
                GameManager.AddPlayer(player);
                Console.WriteLine(name + " is connected");

                ClientHandler client = new ClientHandler();
                client.Start(clientSocket, this, name, player);
            }
        }

        public void Send(string msg)
        {
            foreach (DictionaryEntry Item in Clients)
            {
                TcpClient broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = Encoding.UTF8.GetBytes(msg);
                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }

            Console.WriteLine(msg);
        }
    }

    class ClientHandler
    {
        private TcpClient ClientSocket;
        private NetworkServer Server;
        public string Name;
        private Player Player;

        public void Start(TcpClient clientSocket, NetworkServer server, string name, Player player)
        {
            Player = player;
            Server = server;
            ClientSocket = clientSocket;
            Name = name;
            Thread ctThread = new Thread(Listen);
            ctThread.Start();
        }

        private void Listen()
        {
            byte[] bytesFrom = new byte[10025];

            while ((true))
            {
                try
                {
                    NetworkStream networkStream = ClientSocket.GetStream();
                    int length = networkStream.Read(bytesFrom, 0, (int)ClientSocket.ReceiveBufferSize);
                    string dataFromClient = System.Text.Encoding.UTF8.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, length);
                    Console.WriteLine("From client - " + Name + " : " + dataFromClient);

                    Server.GameManager.CallAction(Player, dataFromClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}