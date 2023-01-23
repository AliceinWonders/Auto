using System;
using System.Threading.Tasks;
using Auto.InfoOwner;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace InfoOwnerServer.Services
{
    public class InfoOwnerService : Informer.InformerBase
    {
        private readonly ILogger<InfoOwnerService> logger;

        public InfoOwnerService(ILogger<InfoOwnerService> logger)
        {
            this.logger= logger;
        }

        public override Task<InfoReply> GetInfo(InfoRequest request, ServerCallContext context)
        {
            Random rnd = new Random();
            return Task.FromResult(new InfoReply() { Income = rnd.Next(20000,100000), Age = rnd.Next(18,80) });
        }
    }
}