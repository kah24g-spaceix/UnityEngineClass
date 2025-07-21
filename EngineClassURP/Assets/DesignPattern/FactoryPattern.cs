using System;

public interface IPrinter 
{
    void Print(String pMessage);
}
public class NetworkPrinter : IPrinter
{
    public void Print (string pMessage)
    {

    }
}

public class ConsolePrinter : IPrinter
{
    public void Print(string pMessage)
    {
        
    }
}

class Test1
{
    void Run()
    {
        IPrinter printer = new ConsolePrinter();
        IPrinter printer2 = new NetworkPrinter();

        // 내가 누굴 쓸지를 바로 알고 있어야함 - 의존성

    }
}
class Test2
{
    void Run()
    {
        void Run(IPrinter pPrinter) // 의존성 주입
        {
            pPrinter.Print("hello world!");
        }
    }
}
/////////////////////////// Simple Factory
///
class SimplePrinterFactory
{
    public IPrinter CreatePrinter(String pType)
    {
        if (pType == "Network")
            return new NetworkPrinter();
        else if (pType == "Console")
            return new ConsolePrinter();

        return null;
    }
}

class Test3
{
    private SimplePrinterFactory m_simplePrinterFactory = new SimplePrinterFactory();

    void Run()
    {
        IPrinter printer = m_simplePrinterFactory.CreatePrinter("Console");
        printer.Print("hello world!");
    }

}

/////// Abstract Factory

interface IPrinterFactory
{
    IPrinter CreatePrinter();
}
class ConsolePrinterFactory : IPrinterFactory
{
    public IPrinter CreatePrinter()
    {
        return new ConsolePrinter();
    }
}
class Test5
{
    private readonly IPrinterFactory m_factory;
    public Test5(IPrinterFactory pFactory)
    {
        m_factory = pFactory;
    }
    void Run()
    {
        IPrinter printer = m_factory.CreatePrinter();
        printer.Print("hello world!");
    }
}

abstract class Test4
{
    void Run()
    {
        IPrinter printer = CreatePrinter();
        printer.Print("hello world!");
    }
    protected abstract IPrinter CreatePrinter();
}
class Test4NetworkPrinter : Test4
{
    protected override IPrinter CreatePrinter()
    {
        return new NetworkPrinter();
    }
}