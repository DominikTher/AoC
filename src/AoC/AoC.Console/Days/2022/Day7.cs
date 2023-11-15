using AoC.Console.Interfaces;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AoC.Console.Days;

internal sealed class Day7 : IDay
{
    public int Year => 2022;

    public int DayNumber => 7;

    public object PartOne(IEnumerable<string> rows)
    {
        IComposite root = new Composite("/", 0);
        var depth = 0;
        var unique = 0;
        IComposite currentLevel = root;

        foreach (var row in rows.Skip(2).Where(row => !string.IsNullOrWhiteSpace(row)))
        {
            if (row.Contains("$ ls"))
            {
                continue;
            }

            if (row.Contains("$ cd .."))
            {
                //var x = root.Search(root, new Composite("vvhmmn"));

                //try
                //{
                //    currentLevel = root.Search(root, 0, --depth) ?? throw new Exception("Invalid node");

                //    if(depth < 0)
                //    {
                //        depth = 0;
                //    }
                //}
                //catch (Exception)
                //{

                //}
                depth--;
                currentLevel = currentLevel?.Parent ?? root;

                continue;
            }

            if (row.StartsWith("$ cd"))
            {
                depth++;
                unique++;
                //currentLevel = root.Search(root, new Composite(row.Split(" ")[2], depth)) ?? throw new Exception("Invalid node");

                try
                {
                    currentLevel = currentLevel.Search(currentLevel, new Composite(row.Split(" ")[2], unique));
                }
                catch (Exception)
                {


                }

                continue;
            }

            var split = row.Split(" ");
            if (split[0].Equals("dir"))
            {
                currentLevel.Add(new Composite(split[1], unique));
            }
            else
            {
                try
                {
                    currentLevel.Add(new Leaf(int.Parse(split[0]), split[1]));
                }
                catch (Exception)
                {

                    //throw;
                }
            }
        }

        var fileSizes = new Dictionary<string, int> { { "/0", 0 } };
        Print(root, fileSizes);

        var r = fileSizes.Where(fS => fS.Value <= 100000).Sum(fS => fS.Value);

        var total = fileSizes["/0"] + 30000000;

        var result = fileSizes.OrderBy(fS => fS.Value).First(fS => total - fS.Value <= 70000000);

        return 0;
    }

    private void Print(IComposite root, IDictionary<string, int> folderSizes)
    {
        foreach (var item in root.Children)
        {
            if (item.IsComposite() && !folderSizes.ContainsKey(item.Name + item.Depth))
            {
                folderSizes[item.Name + item.Depth] = 0;
            }

            if (item.IsComposite() && folderSizes.ContainsKey(item.Name + item.Depth))
            {
                Print(item, folderSizes);

                folderSizes[root.Name + root.Depth] += folderSizes[item.Name + item.Depth];
            }

            if (!item.IsComposite())
            {
                folderSizes[root.Name + root.Depth] += ((IFile)item).FileSize;
            }
        }
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        return 0;
    }
}

public interface IComposite
{
    string Name { get; }
    int Depth { get; }
    IComposite? Parent { get; set; }
    List<IComposite> Children { get; }

    void Add(IComposite composite);

    bool IsComposite();

    IComposite? Search(IComposite root, IComposite composite);
    IComposite? Search(IComposite root, int current, int depth);

    int PrintSize();
}

internal sealed class Composite : IComposite, IDirectory
{
    public List<IComposite> Children { get; }

    public Composite(string directoryName, int depth)
    {
        DirectoryName = directoryName;
        Name = directoryName;
        Children = new();
        Depth = depth;
        Parent = null;
    }

    public string DirectoryName { get; }

    public string Name { get; }

    public int Depth { get; }

    public IComposite? Parent { get; set; }

    public void Add(IComposite composite)
    {
        if (composite.IsComposite())
        {
            composite.Parent = this;
            
        }
        else
        {
            var a = 1;
        }

        Children.Add(composite);
    }

    public bool IsComposite()
        => true;

    public IComposite? Search(IComposite root, IComposite composite)
    {
        foreach (var child in root.Children)
        {
            if (child.IsComposite())
            {
                if (child.Name == composite.Name)
                {
                    return child;
                }

                var result = Search(child, composite);

                //if (result != null)
                //{
                //    return result;
                //}
            }
        }

        return null;
    }

    public IComposite? Search(IComposite root, int current, int depth)
    {
        if (depth == 0)
        {
            return root;
        }

        foreach (var child in root.Children)
        {
            if (child.IsComposite())
            {
                if (child.Depth == depth)
                {
                    return child;
                }

                var result = Search(child, 0, depth);

                if (result != null)
                {
                    return result;
                }
            }
        }

        return null;
    }

    public int PrintSize()
    {
        return 0;
    }
}

internal interface IDirectory
{
    string DirectoryName { get; }
}

internal sealed class Leaf : IComposite, IFile
{
    public List<IComposite> Children => new();

    public Leaf(int fileSize, string fileName)
    {
        FileSize = fileSize;
        FileName = fileName;
        Name = string.Empty;
    }

    public int FileSize { get; }
    public string FileName { get; }

    public string Name { get; }

    public int Depth => -1;

    public IComposite? Parent { get => null; set { } }

    public void Add(IComposite composite)
    {
    }

    public bool IsComposite()
        => false;

    public IComposite? Search(IComposite root, IComposite composite)
        => null;

    public IComposite? Search(IComposite root, int current, int depth)
        => null;

    public int PrintSize()
    {
        return FileSize;
    }
}

internal interface IFile
{
    int FileSize { get; }
    string FileName { get; }
}