using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DNSToHosts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HostsFileOutput.Text = "Enter the domains for which you wish to create a HOSTS file entry on the left, " +
                                   "one per line. Then click the button at the bottom, and a HOSTS file entry will be created " + 
                                   "in this box, using each domain's current IP address. Then simply navigate to your HOSTS " + 
                                   "file and add the text to it in order to override your computer's default DNS resolution and " + 
                                   "route all traffic to that domain to the specified IP address. This can be useful you are unable" + 
                                   " to use DNS for some reason. Empty lines and lines with comments (begin with #) will be preserved. " +
                                   Environment.NewLine + Environment.NewLine +
                                   "Note: Some sites, such as those in the Cloudflare network will require a valid HTTP HOST header " + 
                                   "before you will see the site. Simply use a browser extension like Chrome's ModHeader or Firefox's" + 
                                   "Modify Headers to provide the expected domain name as the HOST header and it should work.";

            DomainList.Text = "# News Sites" + Environment.NewLine;
            DomainList.Text += "cnn.com" + Environment.NewLine;
            DomainList.Text += "foxnews.com" + Environment.NewLine;
            DomainList.Text += "www.nbcnews.com" + Environment.NewLine;
            DomainList.Text += "abcnews.go.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# Search Engines" + Environment.NewLine;
            DomainList.Text += "www.google.com" + Environment.NewLine;
            DomainList.Text += "www.bing.com" + Environment.NewLine;
            DomainList.Text += "www.yahoo.com" + Environment.NewLine;
            DomainList.Text += "duckduckgo.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# Social Media" + Environment.NewLine;
            DomainList.Text += "www.facebook.com" + Environment.NewLine;
            DomainList.Text += "twitter.com" + Environment.NewLine;
            DomainList.Text += "reddit.com" + Environment.NewLine;
            DomainList.Text += "imgur.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# Video Streaming" + Environment.NewLine;
            DomainList.Text += "www.tumblr.com" + Environment.NewLine;
            DomainList.Text += "www.youtube.com" + Environment.NewLine;
            DomainList.Text += "www.dailymotion.com" + Environment.NewLine;
            DomainList.Text += "www.netflix.com" + Environment.NewLine;
            DomainList.Text += "www.hulu.com" + Environment.NewLine;
            DomainList.Text += "www.twitch.tv" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# Online Shopping" + Environment.NewLine;
            DomainList.Text += "www.amazon.com" + Environment.NewLine;
            DomainList.Text += "www.ebay.com" + Environment.NewLine;
            DomainList.Text += "www.newegg.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# General Information" + Environment.NewLine;
            DomainList.Text += "www.wikipedia.org" + Environment.NewLine;
            DomainList.Text += "stackexchange.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# DNS Tools" + Environment.NewLine;
            DomainList.Text += "www.whois.net" + Environment.NewLine;
            DomainList.Text += "whois.domaintools.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# Programmer Resources" + Environment.NewLine;
            DomainList.Text += "stackoverflow.com" + Environment.NewLine;
            DomainList.Text += "developer.mozilla.org" + Environment.NewLine;
            DomainList.Text += "msdn.microsoft.com" + Environment.NewLine;
            DomainList.Text += "github.com" + Environment.NewLine;
            DomainList.Text += "bitbucket.org" + Environment.NewLine;
            DomainList.Text += "nodejs.org" + Environment.NewLine;
            DomainList.Text += "chocolatey.org" + Environment.NewLine;
            DomainList.Text += "news.ycombinator.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;

            DomainList.Text += "# Linux Resources" + Environment.NewLine;
            DomainList.Text += "www.linux.com" + Environment.NewLine;
            DomainList.Text += "www.kernel.org" + Environment.NewLine;
            DomainList.Text += "www.ubuntu.com" + Environment.NewLine;
            DomainList.Text += "www.debian.org" + Environment.NewLine;
            DomainList.Text += "www.linuxmint.com" + Environment.NewLine;
            DomainList.Text += Environment.NewLine;
        }

        private void GenerateHostsFileButtonClicked(object sender, RoutedEventArgs e)
        {
            var domainListLines = DomainList.Text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            HostsFileOutput.Text = "";

            foreach (var line in domainListLines)
            {
                if (line.StartsWith("#") || String.IsNullOrEmpty(line) || String.IsNullOrWhiteSpace(line))
                {
                    HostsFileOutput.Text += line + Environment.NewLine;
                    continue;
                }
                try
                {
                    var ipHostEntry = System.Net.Dns.GetHostEntry(line);
                    var firstIP = ipHostEntry.AddressList.First().ToString();
                    HostsFileOutput.Text += firstIP;
                    HostsFileOutput.Text += " " + line + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    // Swallow exceptions
                    continue;
                }
            }
        }
    }
}
