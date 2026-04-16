using BoschPizza.Models;

namespace BoschPizza.Services;

public static class UserLoginService
{
    static List<UserLogin> UserLogin { get; }
    static int nextId = 3;

    //Busca todos os itens da lista
    public static List<UserLogin> GetAll() => UserLogin;


    //Busca pizza por ID
    public static UserLogin? Get(int id) => UserLogin.FirstOrDefault(p  => p.Id == id);

    //Adicionar nova pizza
    public static void Add(UserLogin userLogin)
    {
        userLogin.Id = nextId++;
        UserLogin.Add(userLogin);
    }

    //Delete
    public static void Delete(int id)
    {
        var userLogin = Get(id);
        if (userLogin is null) return;
        UserLogin.Remove(userLogin);
    }

    //Update
    public static void Update(UserLogin userLogin)
    {
        var index = UserLogin.FindIndex(p => p.Id == userLogin.Id);
        if (index == -1) return;
        UserLogin[index] = userLogin;
    }
    
}