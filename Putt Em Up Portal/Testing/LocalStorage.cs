namespace Putt_Em_Up_Portal.Testing;

public static class LocalStorage<T> where T : new ()
{
    private static LinkedList<T> _list = new();

    public static IEnumerable<T> GetSampleList()
    {

        
       
        return _list;
    }

    public static void SetSampleList(List<T> list)
    {
        if (list != null)
            _list = new LinkedList<T>(list);

    }

    public static void AddToSampleList(T obj)
    {
        _list.AddLast(obj);

    }

}
