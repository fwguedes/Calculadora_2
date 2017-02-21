using CalculadoraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAPI.Interfaces {
    public interface IRabbitAPIProcessor {
        void ConnectServer();
        void ConnectQueue();
        void DefineQoS(uint prefechSize, ushort prefechCount, bool global);
        TResponse SendRPC<TResponse>(Conta conta) where TResponse : class;
    }
}
