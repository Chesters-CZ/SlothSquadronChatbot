// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using GenerickýProgramNetestovaný;

SlothSquadron slothSquadron = new SlothSquadron();

Console.WriteLine("Hello. I am the totally and undeniably real SlothSquadron. I know all the guns.");
Console.WriteLine("You can ask me about any CS:GO weapon attribute:");

while (true)
{
    Console.WriteLine(slothSquadron.Ask(Console.In.ReadLine()));
    Console.WriteLine("");
    Console.WriteLine("Anything else?");
}
