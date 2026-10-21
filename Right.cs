using System;
using System.Threading;

class Program
{     //5 forks 
    static readonly object[] forks = new object[5];
    static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            forks[i]= new object();
        }
        Console.WriteLine("staring dining philosophers\n");

         //threads        
        for (int i = 0; i < 5; i++)
        {
            int professorNumber = i;
            Thread t = new Thread(()=> ProfessorRoutine(professorNumber));
            t.Start();
        }
    }

    static void ProfessorRoutine(int id)
    {
        int leftFork = id;
        int rightFork = (id +1) % 5; // wraps around to 0 for professor 4 

        while(true)
        {
            lock (forks[leftFork])
            {
                
                Console.WriteLine("Professor" + id + "grabbed left fork (" + leftFork +")");
                Thread.Sleep(100);
                lock (forks[rightFork])
                {
                    Console.WriteLine("Professor" + id + "grabbed right fork (" + rightFork + ") and takes a bite");    
                    Thread.Sleep(300);
                }   
            }

            Console.WriteLine("Professor" + id + "put both forks down and is done");
            Thread.Sleep(300);
        }
        
    }
}