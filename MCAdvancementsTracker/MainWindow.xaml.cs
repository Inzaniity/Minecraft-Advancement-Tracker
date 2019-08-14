using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Path = System.Windows.Shapes.Path;

namespace MCAdvancementsTracker
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<Advancement> _advancements = new List<Advancement>();
        OpenFileDialog _browserDialog = new OpenFileDialog();
        private string _path;
        private FileSystemWatcher watcher;

        public MainWindow()
        {
            InitializeComponent();
        }


        public void Prep()
        {
            // Minecraft
            _advancements.Add(new Advancement("Minecraft", "The heart and story of the game", " ", "Have a crafting table in your inventory.", "minecraft:story/root", "Minecraft"));
            _advancements.Add(new Advancement("Stone Age", "Mine stone with your new pickaxe", "Minecraft", "Have cobblestone in your inventory.", "minecraft:story/mine_stone", "Minecraft"));
            _advancements.Add(new Advancement("Getting an Upgrade", "Construct a better pickaxe", "Stone Age", "Have a stone pickaxe in your inventory.", "minecraft:story/upgrade_tools", "Minecraft"));
            _advancements.Add(new Advancement("Acquire Hardware", "Smelt an iron ingot", "Getting an Upgrade", "Have an iron ingot in your inventory.", "minecraft:story/smelt_iron", "Minecraft"));
            _advancements.Add(new Advancement("Suit Up", "Protect yourself with a piece of iron armor", "Acquire Hardware", "Have any type of iron armor in your inventory.", "minecraft:story/obtain_armor", "Minecraft"));
            _advancements.Add(new Advancement("Hot Stuff", "Fill a bucket with lava", "Acquire Hardware", "Have a lava bucket in your inventory.", "minecraft:story/lava_bucket", "Minecraft"));
            _advancements.Add(new Advancement("Isn't It Iron Pick", "Upgrade your pickaxe", "Acquire Hardware", "Have an iron pickaxe in your inventory.", "minecraft:story/iron_tools", "Minecraft"));
            _advancements.Add(new Advancement("Not Today, Thank You", "Deflect an arrow with a shield", "Suit Up", "Deflect any projectile with a shield.", "minecraft:story/deflect_arrow", "Minecraft"));
            _advancements.Add(new Advancement("Ice Bucket Challenge", "Form and mine a block of Obsidian", "Hot Stuff", "Have a block of obsidian in your inventory.", "minecraft:story/form_obsidian", "Minecraft"));
            _advancements.Add(new Advancement("Diamonds!", "Acquire diamonds", "Isn't It Iron Pick", "Have a diamond in your inventory.", "minecraft:story/mine_diamond", "Minecraft"));
            _advancements.Add(new Advancement("We Need to Go Deeper", "Build, light and enter a Nether Portal", "Ice Bucket Challenge", "Enter the Nether dimension.", "minecraft:story/enter_the_nether", "Minecraft"));
            _advancements.Add(new Advancement("Cover Me With Diamonds", "Diamond armor saves lives", "Diamonds!", "Have any type of diamond armor in your inventory.", "minecraft:story/shiny_gear", "Minecraft"));
            _advancements.Add(new Advancement("Enchanter", "Enchant an item at an Enchanting Table", "Diamonds!", " ", "minecraft:story/enchant_item", "Minecraft"));
            _advancements.Add(new Advancement("Zombie Doctor", "Weaken and then cure a Zombie Villager", "We Need to Go Deeper", "Throw a splash potion of weakness at a zombie villager and give it a golden apple (by facing the zombie and pressing the use key with a golden apple in your hand), then wait for the villager to be cured.", "minecraft:story/cure_zombie_villager", "Minecraft"));
            _advancements.Add(new Advancement("Eye Spy", "Follow an Eye of Ender", "We Need to Go Deeper", "Enter a stronghold.", "minecraft:story/follow_ender_eye", "Minecraft"));
            _advancements.Add(new Advancement("The End?", "Enter the End Portal", "Eye Spy", "Enter the End dimension.", "minecraft:story/enter_the_end", "Minecraft"));

            // Nether
            _advancements.Add(new Advancement("Nether", "Bring summer clothes", " ", "Enter the Nether dimension.", "minecraft:nether/root", "Nether"));
            _advancements.Add(new Advancement("Return to Sender", "Destroy a Ghast with a fireball", "Nether", "Kill a ghast using a ghast fireball.", "minecraft:nether/return_to_sender", "Nether"));
            _advancements.Add(new Advancement("Subspace Bubble", "Use the Nether to travel 7 km in the Overworld", "Nether", "Use the Nether to travel between 2 points in the Overworld with a minimum horizontal distance of 7000 blocks between each other, about 875 blocks in the Nether.", "minecraft:nether/fast_travel", "Nether"));
            _advancements.Add(new Advancement("A Terrible Fortress", "Break your way into a Nether Fortress", "Nether", "Enter a Nether fortress.", "minecraft:nether/find_fortress", "Nether"));
            _advancements.Add(new Advancement("Uneasy Alliance", "Rescue a Ghast from the Nether, bring it safely home to the Overworld... and then kill it", "Return to Sender", "Kill a Ghast in the Overworld.", "minecraft:nether/uneasy_alliance", "Nether"));
            _advancements.Add(new Advancement("Spooky Scary Skeleton", "Obtain a Wither Skeleton's skull", "A Terrible Fortress", "Have a wither skeleton skull in your inventory.", "minecraft:nether/get_wither_skull", "Nether"));
            _advancements.Add(new Advancement("Into Fire", "Relieve a Blaze of its rod", "A Terrible Fortress", "Have a blaze rod in your inventory.", "minecraft:nether/obtain_blaze_rod", "Nether"));
            _advancements.Add(new Advancement("Withering Heights", "Summon the Wither", "Spooky Scary Skeleton", "Be within a 100.9 100.9 103.5 cuboid centered on the Wither when it is spawned.", "minecraft:nether/summon_wither", "Nether"));
            _advancements.Add(new Advancement("Local Brewery", "Brew a potion", "Into Fire", "Pick up an item from a brewing stand potion slot (Note that this does not need to be a potion. Water bottles or even empty bottles also trigger this advancement).", "minecraft:nether/brew_potion", "Nether"));
            _advancements.Add(new Advancement("Bring Home the Beacon", "Construct and place a Beacon", "Withering Heights", "Be within a 20 20 14 cuboid centered on a beacon block when it realizes it has become powered.", "minecraft:nether/create_beacon", "Nether"));
            _advancements.Add(new Advancement("A Furious Cocktail", "Have every potion effect applied at the same time", "Local Brewery", "Have all of these 13 potion effects applied to the player at the same time.", "minecraft:nether/all_potions", "Nether"));
            _advancements.Add(new Advancement("Beaconator", "Bring a beacon to full power", "Bring Home the Beacon", "Be within a 20 20 14 cuboid centered on a beacon block when it realizes it is being powered by a size 4 pyramid.", "minecraft:nether/create_full_beacon", "Nether"));
            _advancements.Add(new Advancement("How Did We Get Here?", "Have every effect applied at the same time", "A Furious Cocktail", "Have all of these 26 effects applied to the player at the same time.", "minecraft:nether/all_effects", "Nether"));

            // The End
            _advancements.Add(new Advancement("The End", "Or the beginning?", " ", "Enter the End dimension.", "minecraft:end/root", "The End"));
            _advancements.Add(new Advancement("Free the End", "Good luck", "The End", "Kill the ender dragon.", "minecraft:end/kill_dragon", "The End"));
            _advancements.Add(new Advancement("The Next Generation", "Hold the Dragon Egg", "Free the End", "Have a dragon egg in your inventory.", "minecraft:end/dragon_egg", "The End"));
            _advancements.Add(new Advancement("Remote Getaway", "Escape the island", "Free the End", "Throw an ender pearl through or walk into an End gateway.", "minecraft:end/enter_end_gateway", "The End"));
            _advancements.Add(new Advancement("The End... Again...", "Respawn the Ender Dragon", "Free the End", "Summon an ender dragon using ender crystals.", "minecraft:end/respawn_dragon", "The End"));
            _advancements.Add(new Advancement("You Need a Mint", "Collect dragon's breath in a glass bottle.", "Free the End", "Have a bottle of dragon's breath in your inventory.", "minecraft:end/dragon_breath", "The End"));
            _advancements.Add(new Advancement("The City at the End of the Game", "Go on in, what could happen?", "Remote Getaway", "Enter an End city.", "minecraft:end/find_end_city", "The End"));
            _advancements.Add(new Advancement("Sky's the Limit", "Find Elytra", "The City at the End of the Game", "Have a pair of elytra in your inventory.", "minecraft:end/elytra", "The End"));
            _advancements.Add(new Advancement("Great View From Up Here", "Levitate up 50 blocks from the attacks of a Shulker", "The City at the End of the Game", "Move a vertical distance of 50 blocks with the levitation effect applied.", "minecraft:end/levitate", "The End"));

            // Adventure
            _advancements.Add(new Advancement("Adventure", "Adventure, exploration, and combat", " ", "Kill any entity, or be killed by any entity.", "minecraft:adventure/root", "Adventure"));
            _advancements.Add(new Advancement("Voluntary Exile", "Kill a raid captain.", "Adventure", "Obtain an Ominous Banner, whether from a Pillager Outpost or from a Raid Captain.", "minecraft:adventure/voluntary_exile", "Adventure"));
            _advancements.Add(new Advancement("Monster Hunter", "Kill any hostile monster", "Adventure", "Kill one of these 25 mobs. Other mobs are ignored for this advancement.", "minecraft:adventure/kill_a_mob", "Adventure"));
            _advancements.Add(new Advancement("What a Deal!", "Successfully trade with a Villager", "Adventure", "Take an item from a villager's trading output slot, and put it in your inventory.", "minecraft:adventure/trade", "Adventure"));
            _advancements.Add(new Advancement("Ol' Betsy", "Shoot a crossbow", "Adventure", " ", "minecraft:adventure/ol_betsy", "Adventure"));
            _advancements.Add(new Advancement("Sweet Dreams", "Change your respawn point", "Adventure", "Lie down in a bed. The advancement will be granted as soon as the player is in the bed, even if the player does not successfully sleep.", "minecraft:adventure/sleep_in_bed", "Adventure"));
            _advancements.Add(new Advancement("Hero of the Village", "Successfully defend a village from a raid", "Voluntary Exile", " ", "minecraft:adventure/hero_of_the_village", "Adventure"));
            _advancements.Add(new Advancement("A Throwaway Joke", "Throw a trident at something.", "Monster Hunter", "Hit something with a trident", "minecraft:adventure/throw_trident", "Adventure"));
            _advancements.Add(new Advancement("Take Aim", "Shoot something with a bow and arrow", "Monster Hunter", " ", "minecraft:adventure/shoot_arrow", "Adventure"));
            _advancements.Add(new Advancement("Monsters Hunted", "Kill one of every hostile monster", "Monster Hunter", "Kill each of these 25 mobs. Other mobs may be killed, but are ignored for the advancement.", "minecraft:adventure/kill_all_mobs", "Adventure"));
            _advancements.Add(new Advancement("Postmortal", "Use a Totem of Undying to cheat death", "Monster Hunter", " ", "minecraft:adventure/totem_of_undying", "Adventure"));
            _advancements.Add(new Advancement("Hired Help", "Summon an Iron Golem to help defend a village", "What a Deal!", "Summon an iron golem.", "minecraft:adventure/summon_iron_golem", "Adventure"));
            _advancements.Add(new Advancement("Two Birds, One Arrow", "Kill two Phantoms with a piercing arrow", "Ol' Betsy", " ", "minecraft:adventure/two_birds_one_arrow", "Adventure"));
            _advancements.Add(new Advancement("Who's the Pillager Now?", "Give a Pillager a taste of their own medicine", "Ol' Betsy", "Kill a pillager with a crossbow.", "minecraft:adventure/whos_the_pillager_now", "Adventure"));
            _advancements.Add(new Advancement("Arbalistic", "Kill five unique mobs with one crossbow shot", "Ol' Betsy", " ", "minecraft:adventure/arbalistic", "Adventure"));
            _advancements.Add(new Advancement("Adventuring Time", "Discover every biome", "Sweet dreams", "Visit all of these 42 biomes. Other biomes may also be visited, but are ignored for the advancement.", "minecraft:adventure/adventuring_time", "Adventure"));
            _advancements.Add(new Advancement("Very Very Frightening", "Strike a Villager with lightning", "A Throwaway Joke", "Hit a villager with lightning created by a trident with the Channeling enchantment.", "minecraft:adventure/very_very_frightening", "Adventure"));
            _advancements.Add(new Advancement("Sniper Duel", "Kill a Skeleton from more than 50 meters away", "Take Aim", "Kill a skeleton with a projectile while being at least 50 blocks away horizontally.", "minecraft:adventure/sniper_duel", "Adventure"));

            // Husbandry
            _advancements.Add(new Advancement("Husbandry", "The world is full of friends and food", " ", "Eat anything that can be eaten.", "minecraft:husbandry/root", "Husbandry"));
            _advancements.Add(new Advancement("The Parrots and the Bats", "Breed two animals together", "Husbandry", " ", "minecraft:husbandry/breed_an_animal", "Husbandry"));
            _advancements.Add(new Advancement("Best Friends Forever", "Tame an animal", "Husbandry", " ", "minecraft:husbandry/tame_an_animal", "Husbandry"));
            _advancements.Add(new Advancement("Fishy Business", "Catch a fish", "Husbandry", "Use a fishing rod to catch a fish.", "minecraft:husbandry/fishy_business", "Husbandry"));
            _advancements.Add(new Advancement("A Seedy Place", "Plant a seed and watch it grow", "Husbandry", "Plant one of these 5 seeds. Crops and plants without seeds are ignored for the advancement.", "minecraft:husbandry/plant_seed", "Husbandry"));
            _advancements.Add(new Advancement("Two by Two", "Breed all the animals!", "The Parrots and the Bats", "Breed pairs of each of these 14 mobs. Other breedable mobs, if any, are ignored for the advancement.", "minecraft:husbandry/bred_all_animals", "Husbandry"));
            _advancements.Add(new Advancement("A Complete Catalogue", "Tame all cat variants!", "Best Friends Forever", " ", "minecraft:husbandry/complete_catalogue", "Husbandry"));
            _advancements.Add(new Advancement("Tactical Fishing", "Catch a fish... without a fishing rod!", "Fishy Business", "Use a water bucket on a fish mob to get a bucket of fish.", "minecraft:husbandry/tactical_fishing", "Husbandry"));
            _advancements.Add(new Advancement("A Balanced Diet", "Eat everything that is edible, even if it's not good for you", "A Seedy Place", "Eat each of these 38 foods. Other foods are ignored for the advancement.", "minecraft:husbandry/balanced_diet", "Husbandry"));
            _advancements.Add(new Advancement("Serious Dedication", "Completely use up a diamond hoe, and then reevaluate your life choices", "A Seedy Place", "Exhaust the durability of a diamond hoe, breaking it.", "minecraft:husbandry/break_diamond_hoe", "Husbandry"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _path = Environment.SpecialFolder.ApplicationData.ToString();
            Prep();
            var minecraft = new StackPanel { Orientation = System.Windows.Controls.Orientation.Vertical };
            var nether = new StackPanel { Orientation = System.Windows.Controls.Orientation.Vertical };
            var theEnd = new StackPanel { Orientation = System.Windows.Controls.Orientation.Vertical };
            var adventure = new StackPanel { Orientation = System.Windows.Controls.Orientation.Vertical };
            var husbandry = new StackPanel { Orientation = System.Windows.Controls.Orientation.Vertical };

            foreach (Advancement advancement in _advancements)
            {
                switch (advancement.Category)
                {
                    case "Minecraft":
                        minecraft.Children.Add(new System.Windows.Controls.CheckBox { Name = advancement.Id.Replace(":", "_").Replace("/", "_"), Content = advancement.Name + " (" + advancement.Id + ")", ToolTip = advancement.Description, Padding = new Thickness(5) });
                        break;
                    case "Nether":
                        nether.Children.Add(new System.Windows.Controls.CheckBox { Name = advancement.Id.Replace(":", "_").Replace("/", "_"), Content = advancement.Name + " (" + advancement.Id + ")", ToolTip = advancement.Description, Padding = new Thickness(5) });
                        break;
                    case "The End":
                        theEnd.Children.Add(new System.Windows.Controls.CheckBox { Name = advancement.Id.Replace(":", "_").Replace("/", "_"), Content = advancement.Name + " (" + advancement.Id + ")", ToolTip = advancement.Description, Padding = new Thickness(5) });
                        break;
                    case "Adventure":
                        adventure.Children.Add(new System.Windows.Controls.CheckBox { Name = advancement.Id.Replace(":", "_").Replace("/", "_"), Content = advancement.Name + " (" + advancement.Id + ")", ToolTip = advancement.Description, Padding = new Thickness(5) });
                        break;
                    case "Husbandry":
                        husbandry.Children.Add(new System.Windows.Controls.CheckBox { Name = advancement.Id.Replace(":", "_").Replace("/", "_"), Content = advancement.Name + " (" + advancement.Id + ")", ToolTip = advancement.Description, Padding = new Thickness(5) });
                        break;
                    default:
                        break;
                }

            }

            CntMinecraft.Content = minecraft;
            CntNether.Content = nether;
            CntEnd.Content = theEnd;
            CntAdventure.Content = adventure;
            CntHusbandry.Content = husbandry;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(_path));

        }

        private void Btn_path_Click(object sender, RoutedEventArgs e)
        {
            _browserDialog.Title = "Advancements JSON";
            _browserDialog.InitialDirectory = Environment.SpecialFolder.ApplicationData.ToString();

            if (_browserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _path = _browserDialog.FileName;

                watcher = new FileSystemWatcher
                {
                    Path = System.IO.Path.GetDirectoryName(_path),
                    NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                    Filter = "*.json",
                    EnableRaisingEvents = true
                };
                watcher.Changed += new FileSystemEventHandler(OnChanged);

                tb_path.Text = _path;
            }
        }
    }
}
