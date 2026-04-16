using BoschPizza.Models;

namespace BoschPizza.Services;

public static class ClienteService
{
    static List<Cliente> Cliente { get; }
    static int nextId = 3;

    //Método Construtor
    static ClienteService()
    {
        Cliente = new List<Cliente>
        {
            new Cliente{ Id= 1, Name = "Daniely", Address = "Rua 15 de novembro", Phone = "47997-6789" },  
            new Cliente{ Id= 1, Name = "Kaua", Address = "Rua joao ramalho", Phone = "477564-6439" }
        };
    }

    //Busca todos os itens da lista
    public static List<Cliente> GetAll() => Cliente;


    //Busca pizza por ID
    public static Cliente? Get(int id) => Cliente.FirstOrDefault(p  => p.Id == id);

    //Adicionar nova pizza
    public static void Add(Cliente cliente)
    {
        cliente.Id = nextId++;
        Cliente.Add(cliente);
    }

    //Delete
    public static void Delete(int id)
    {
        var cliente = Get(id);
        if (cliente is null) return;
        Cliente.Remove(cliente);
    }

    //Update
    public static void Update(Cliente cliente)
    {
        var index = Cliente.FindIndex(p => p.Id == cliente.Id);
        if (index == -1) return;
        Cliente[index] = cliente;
    }
    
}