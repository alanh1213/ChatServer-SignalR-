using Microsoft.AspNetCore.SignalR.Client;


var connection = new HubConnectionBuilder().WithUrl("https://localhost:7232/chathub").Build(); 

void HandleMessage(string user, string message) //Armar cuerpo de mensaje
{
    Console.WriteLine($"{user}: {message}");
}

connection.On<string, string>("ReceiveMessage", HandleMessage); //Si se reciben mensaje. Imprimir en pantalla.

await connection.StartAsync(); //Conexion al hub

Console.WriteLine("Conectado. Ingrese su nombre: ");
var userName = Console.ReadLine();

while (true) //Si hay conexion, pide nombre, escribe mensaje y se envia al hub a todos los conectados
{
    var message = Console.ReadLine();

    if (string.IsNullOrEmpty(message)) break;

    await connection.SendAsync("SendMessage", userName, message);
}

await connection.DisposeAsync();