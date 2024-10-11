namespace DesignPatterns.Behavioral.Observer.BadExample;

public class BadDatasource
{
    private List<int> _values;
    public IEnumerable<int> Values
    { 
        get => _values;
        set
        {
            foreach(object dependentObject in _dependentObjects)
            {
                if (dependentObject is BadBarchart)
                {
                    ((BadBarchart)dependentObject).Render(_values);
                }
                else if (dependentObject is Spreadsheet)
                {
                    (dependentObject as Spreadsheet).CalculateTotal(_values);
                }
            }
        }
    }

    private List<object> _dependentObjects;

    public BadDatasource(IEnumerable<object> dependentObjects)
    {
        _dependentObjects = new List<object>();
        _dependentObjects.AddRange(dependentObjects);
    }
}