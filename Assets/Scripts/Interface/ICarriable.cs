using System.Collections;

namespace Interface
{
    public interface ICarriable{
        IEnumerator Carry(Human human);
        IEnumerator Hold(Human human);
    }
}
