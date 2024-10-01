namespace DesignPatterns.Behavioral.TemplateMethod;

public interface IBeverageBrewer
{
    void Prepare(bool addCondiments = false);
}