using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Tiles.Resources;

namespace Tiles
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();        
        }

        private void btnFlip_Click_1(object sender, RoutedEventArgs e)
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();

            FlipTileData fliptile = new FlipTileData();
            fliptile.Title = "michaelcrump.net";
            fliptile.Count = 33;
            fliptile.BackTitle = "back title";

            fliptile.BackContent = "back content";
            fliptile.WideBackContent = "back of the wide tile content";

            fliptile.SmallBackgroundImage = new Uri("Assets/Tiles/FlipCycleTileSmall.png", UriKind.Relative);
            fliptile.BackgroundImage = new Uri("Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative);
            fliptile.WideBackgroundImage = new Uri("Assets/Tiles/FlipCycleTileLarge.png", UriKind.Relative);

            ShellTile.Create(new Uri("/MainPage.xaml?id=flip", UriKind.Relative), fliptile, true);

            //tile.Update(fliptile);

        }

        private void btnIconic_Click_1(object sender, RoutedEventArgs e)
        {

            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();

            IconicTileData icontile = new IconicTileData();
            icontile.Title = "michaelcrump.net";
            icontile.Count = 33;

            icontile.IconImage = new Uri("Assets/Tiles/IconicTileMediumLarge.png", UriKind.Relative);
            icontile.SmallIconImage = new Uri("Assets/Tiles/IconicTileSmall.png", UriKind.Relative);

            icontile.WideContent1 = "content 1";
            icontile.WideContent2 = "content 2";
            icontile.WideContent3 = "content 3";

            ShellTile.Create(new Uri("/MainPage.xaml", UriKind.Relative), icontile, true);

        }

        private void btnCycle_Click_1(object sender, RoutedEventArgs e)
        {
            CycleTileData cycleicon = new CycleTileData();
            cycleicon.Title = "michaelcrump.net";
            cycleicon.Count = 33;

            cycleicon.SmallBackgroundImage = new Uri("Assets/Tiles/FlipCycleTileSmall.png", UriKind.Relative);

            List<Uri> images = new List<Uri>();
            for (int i = 1; i < 8; i++)
            {
                images.Add(new Uri("Images/" + i + ".jpg", UriKind.Relative));
            }

            cycleicon.CycleImages = images;

            ShellTile.Create(new Uri("/MainPage.xaml?id=cycle", UriKind.Relative), cycleicon, true);
        }
    }
}