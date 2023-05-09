using Azure.Messaging.ServiceBus;

namespace MvcLogicApps.Services
{
    public class ServiceQueueBus
    {

        private ServiceBusClient client;
        private List<string> Mensajes;

        public ServiceQueueBus(ServiceBusClient client)
        {
            this.client = client;
            this.Mensajes = new List<string>();
        }

        public async Task SendMessageAsync(string data)
        {
            //PARA ENVIAR UN MENSAJE, NECESITAMOS UN SENDER
            ServiceBusSender sender =
                this.client.CreateSender("developers");
            ServiceBusMessage message =
                new ServiceBusMessage(data);
            await sender.SendMessageAsync(message);
        }

        //LA RECEPCION DE MENSAJES NO ES INMEDIATA
        //ESTO QUIERE DECIR QUE IRA LEYENDO MENSAJES SEGUN PUEDA
        public async Task<List<string>> ReceiveMessagesAsync()
        {
            ServiceBusProcessor processor =
                this.client.CreateProcessor("developers");
            processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
            processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
            //INICIAMOS LA ESCUCHA DEL TOPIC/QUEUE
            await processor.StartProcessingAsync();
            //COMO ESTAMOS CON PRUEBAS, DORMIMOS UN POCO EL PROCESO
            //DE ESCUCHA PARA QUE PUEDA RELLENAR LA LISTA
            Thread.Sleep(30000);
            await processor.StopProcessingAsync();
            return this.Mensajes;
        }

        private async
            Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            string content = arg.Message.Body.ToString();
            this.Mensajes.Add(content);
            //PODEMOS INDICAR QUE HEMOS PROCESADO EL MENSAJE
            await arg.CompleteMessageAsync(arg.Message);
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            return Task.CompletedTask;
        }

    }

}