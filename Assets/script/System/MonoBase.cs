using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBase : MonoBehaviour
{
  public Dictionary<int, object> container = new Dictionary<int, object>();
  
  // добавляем в контейнер объект определенного типа
  public void Add<T>(T o)
  {
   container.Add(typeof(T).GetHashCode(), o);
  }
  
  // вытаскиваем из контейнера объект определенного типа
  public T Get<T>()
  {
   object val;
   if (container.TryGetValue(typeof(T).GetHashCode(), out val))
   {
    return (T) val;
   }

   return default(T);
  }
  
}
