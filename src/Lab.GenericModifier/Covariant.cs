using System.Text;

namespace Lab.GenericModifier;

interface ICovariant<out T> { }
interface IExtCovariant<out T> : ICovariant<T> { }
 
// Implementing covariant interface.
class SampleCovariant<T> : ICovariant<T> { }
 
public class CovariantDemo
{
    public static void Show()
    {
        string description = new StringBuilder()
            .AppendLine("Covariant<out T>")
            .AppendLine("ICovariant<Object> =  new SampleCovariant<String>()")
            .ToString();

        Console.WriteLine(description);

        ICovariant<Object> iobj = new SampleCovariant<Object>();
        ICovariant<String> istr = new SampleCovariant<String>();
 
        // You can assign istr to iobj because
        // the ICovariant interface is covariant.
        iobj = istr;

        ICovariant<Object> iobj2 =  new SampleCovariant<String>();
        //ICovariant<String> istr2 = new SampleCovariant<Object>();//ERROR
    }
}