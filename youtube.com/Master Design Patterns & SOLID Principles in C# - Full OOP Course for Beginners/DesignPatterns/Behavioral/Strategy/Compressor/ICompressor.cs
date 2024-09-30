namespace DesignPatterns.Behavioral.Strategy.Compressor;

public interface ICompressor
{
    string FileExtension { get; }

    void Compress();
}