using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_TestTack2
{
    internal class Program
    {
        static class Server
        {
            static int count;
            static ReaderWriterLock rwl = new ReaderWriterLock();

            public static void GetCount()
            {
                rwl.AcquireReaderLock(Timeout.InfiniteTimeSpan);
                try
                {
                    Console.WriteLine(count);
                }
                finally
                {
                    rwl.ReleaseReaderLock();
                }
            }

            public static void AddToCount()
            {
                rwl.AcquireWriterLock(Timeout.InfiniteTimeSpan);
                count++;
                rwl.ReleaseWriterLock();
            }
        }

        void asd()
        {

        }
        static void Main(string[] args)
        {
           
        }
    }
}
