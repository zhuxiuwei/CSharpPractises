using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
class ManualResetEventSlim_ProducerConsumer
{
    private static List<int> resource = new List<int>();
    private static int maxCount = 10;
    private static int currentCount = 0;
    private static ManualResetEventSlim producerMRE = new ManualResetEventSlim(false); // initialize as unsignaled
    private static ManualResetEventSlim consumerMRE = new ManualResetEventSlim(false); // initialize as unsignaled


    public static void SimpleProducerConsumer()
    {
        Task.Run(() => consume());
        Task.Run(() => produce());
    }

    private static void produce()
    {
        while (currentCount < maxCount)
        {

            while (resource.Count > 0)  //No need to produce. Waiting for consumer to consume.
            {
                producerMRE.Wait();
                consumerMRE.Set();
            }

            //produce
            Console.WriteLine("Producer produce " + ++currentCount);
            resource.Add(currentCount);
            
            producerMRE.Reset();
            consumerMRE.Set();
        }
        
    }

    private static void consume()
    {
        while (currentCount <= maxCount)
        {
            while (resource.Count == 0) //No resource to consume. Wait for producer to produce.
            {
                consumerMRE.Wait();
                producerMRE.Set();
            }

            //consume
            Console.WriteLine("Consumer consume " + resource[0]);
            resource.Clear();
            
consumerMRE.Reset();
            producerMRE.Set();
        }
    }

}