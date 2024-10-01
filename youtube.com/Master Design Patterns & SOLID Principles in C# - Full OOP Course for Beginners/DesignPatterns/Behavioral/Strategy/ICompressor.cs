namespace DesignPatterns.Behavioral.Strategy;

public interface ICompressor
{
    string FileExtension { get; }

    void Compress();
}