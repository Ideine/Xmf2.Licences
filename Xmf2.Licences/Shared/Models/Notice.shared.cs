namespace Xmf2.Licences.Models
{
    public class Notice
    {
        public string Name { get; }
        public string Url { get; }
        public string Copyright { get; }
        public Licence License { get; }

        public Notice(string name, string url, string copyright, Licence license)
        {
            Name = name;
            Url = url;
            Copyright = copyright;
            License = license;
        }
    }
}