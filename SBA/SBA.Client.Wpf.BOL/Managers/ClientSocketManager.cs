﻿using SBA.BOL.Common.Factory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace SBA.Client.Wpf.BOL.Managers
{
    public interface IClientSocketManager
    {
        List<Dictionary<string, string>> GetFaqsAnswers();
        List<Dictionary<string, string>> GetFaqsQuestions();
        void DeleteQuestion(string id);
        void DeleteAnswer(string id);
        void EditAnswer(string id, string answer);
        void EditQuestion(string id, string answerId, string question);
        void AddQuestion(string question, string answerId);
    }

    public class ClientSocketManager : IClientSocketManager
    {
        public List<Dictionary<string, string>> GetFaqsAnswers()
        {
            var sendDictionary = new Dictionary<string, string>
            {
                { "AuthGuid", app.Default.authGuid },
                { "Type", "App" },
                { "Request", "FaqDataAnswers" }
            };

            var dataFromCore = ExchangeDataWithCore(sendDictionary);
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>(dataFromCore))
                return (List<Dictionary<string, string>>) binaryFormatter.Deserialize(memoryStream);
        }

        public List<Dictionary<string, string>> GetFaqsQuestions()
        {
            var sendDictionary = new Dictionary<string, string>
            {
                { "AuthGuid", app.Default.authGuid },
                { "Type", "App" },
                { "Request", "FaqDataQuestions" }
            };

            var dataFromCore = ExchangeDataWithCore(sendDictionary);
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            using (var memoryStream = SimpleFactory.Get<MemoryStream>(dataFromCore))
                return (List<Dictionary<string, string>>)binaryFormatter.Deserialize(memoryStream);
        }

        public byte[] ExchangeDataWithCore(Dictionary<string, string> dictionary)
        {
            byte[] sendBytes = null;
            string coreHost = app.Default.coreHost;
            string corePort = app.Default.corePort;
            var binaryFormatter = SimpleFactory.Get<BinaryFormatter>();
            var clientSocket = SimpleFactory
                .Get<Socket>(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.IP);

            using (var memoryStream = SimpleFactory.Get<MemoryStream>())
            {
                binaryFormatter.Serialize(memoryStream, dictionary);
                sendBytes = memoryStream.ToArray();
            }

            clientSocket.Connect(coreHost, Convert.ToInt32(corePort));
            clientSocket.Send(sendBytes);

            byte[] serverData = new byte[clientSocket.ReceiveBufferSize];
            clientSocket.Receive(serverData);
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

            return serverData;
        }

        public void DeleteQuestion(string id)
        {
            var sendDictionary = new Dictionary<string, string>
            {
                { "AuthGuid", app.Default.authGuid },
                { "Type", "App" },
                { "Request", "FaqDeleteQuestion" },
                { "Id", id }
            };

            var dataFromCore = ExchangeDataWithCore(sendDictionary);
        }

        public void DeleteAnswer(string id)
        {
            var sendDictionary = new Dictionary<string, string>
            {
                { "AuthGuid", app.Default.authGuid },
                { "Type", "App" },
                { "Request", "FaqDeleteAnswer" },
                { "Id", id }
            };

            var dataFromCore = ExchangeDataWithCore(sendDictionary);
        }

        public void EditAnswer(string id, string answer)
        {
            var sendDictionary = new Dictionary<string, string>
            {
                { "AuthGuid", app.Default.authGuid },
                { "Type", "App" },
                { "Request", "FaqEditAnswer" },
                { "Id", id },
                { "Answer", answer }
            };

            var dataFromCore = ExchangeDataWithCore(sendDictionary);
        }

        public void EditQuestion(string id, string answerId, string question)
        {
            var sendDictionary = new Dictionary<string, string>
            {
                { "AuthGuid", app.Default.authGuid },
                { "Type", "App" },
                { "Request", "FaqEditQuestion" },
                { "Id", id },
                { "AnswerId", answerId },
                { "Question", question }
            };

            var dataFromCore = ExchangeDataWithCore(sendDictionary);
        }

        public void AddQuestion(string question, string answerId)
        {
            var sendDictionary = new Dictionary<string, string>
            {
                { "AuthGuid", app.Default.authGuid },
                { "Type", "App" },
                { "Request", "FaqAddQuestion" },
                { "AnswerId", answerId },
                { "Question", question }
            };

            var dataFromCore = ExchangeDataWithCore(sendDictionary);
        }
    }
}
