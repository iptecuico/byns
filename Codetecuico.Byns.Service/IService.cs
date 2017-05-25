namespace Codetecuico.Byns.Service
{
    public interface IService<T> where T : class
    { 
        T Add(T data);
        bool Update(T data);
        T GetById(int id);
    }
}
