using System.Text;

namespace Lab.GenericModifier;

public interface IContravariant<in T> { }
public interface IExtContravariant<in T> : IContravariant<T> { }
 




public class SampleContravariant<T> : IContravariant<T> { }
 
public class ContravariantDemo
{
    public static void Show()
    {
        string description = new StringBuilder()
            .AppendLine("Contravariant<in T>")
            .AppendLine("IContravariant<String> = new Sample<Object>()")
            .ToString();

        Console.WriteLine(description);
        

        IContravariant<Object> iobj = new SampleContravariant<Object>();
        IContravariant<String> istr = new SampleContravariant<String>();

        // You can assign iobj to istr because
        // the IContravariant interface is contravariant.
        istr = iobj;

        
        IContravariant<String> istr2 = new SampleContravariant<Object>();
        //IContravariant<Object> istr3 = new SampleContravariant<String>(); //ERROR
    }
}