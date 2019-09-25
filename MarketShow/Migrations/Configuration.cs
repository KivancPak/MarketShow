namespace MarketShow.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MarketShow.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MarketShow.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MarketShow.Models.ApplicationDbContext context)
        {
            // admin rol� yoksa olu�tur
            if (!context.Roles.Any(x => x.Name == "admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "admin" };
                roleManager.Create(role);
            }

            // admin kullan�c�s� yoksa olu�tur ve admin rol� ata
            if (!context.Users.Any(x => x.UserName == "admin@example.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser
                {
                    Email = "admin@example.com",
                    UserName = "admin@example.com",
                    Ad = "Admin",
                    Soyad = "User",
                    EmailConfirmed = true
                };

                userManager.Create(user, "Ankara1.");
                userManager.AddToRole(user.Id, "admin");
            }

            // �rnek kategorileri ve �r�nleri ekle
            if (!context.Kategoriler.Any())
            {
                var kategori1 = new Kategori
                {
                    KategoriAd = "��ecekler"
                };

                kategori1.Urunler.Add(new Urun
                {
                    UrunAd = "Kola",
                    BirimFiyat = 3.5m,
                    Aciklama = "Serinleten lezzet. So�uk i�iniz."
                });

                kategori1.Urunler.Add(new Urun
                {
                    UrunAd = "Ayran",
                    BirimFiyat = 3.5m,
                    Aciklama = "Milli i�ece�imiz. Ben ko�ar�m anlam�na gelir."
                });

                context.Kategoriler.Add(kategori1);

                context.Kategoriler.Add(new Kategori
                {
                    KategoriAd = "S�t �r�nleri"
                });

                var kategori3 = new Kategori
                {
                    KategoriAd = "Cep Telefonlar�"
                };

                kategori3.Urunler.Add(new Urun
                {
                    UrunAd = "IPhone X",
                    BirimFiyat = 8000m,
                    Aciklama = "Bir telefondan �ok daha fazlas�."
                });

                context.Kategoriler.Add(kategori3);

            }

            // �ehirleri ekle
            if (!context.Sehirler.Any())
            {
                foreach (KeyValuePair<int, string> entry in TurkiyeIller())
                {
                    context.Sehirler.Add(
                        new Models.Sehir { Id = entry.Key, SehirAd = entry.Value }
                    );
                }
            }
        }

        private Dictionary<int, string> TurkiyeIller()
        {
            return new Dictionary<int, string>
                {
                    {1, "Adana"},
                    {2, "Ad�yaman"},
                    {3, "Afyonkarahisar"},
                    {4, "A�r�"},
                    {5, "Amasya"},
                    {6, "Ankara"},
                    {7, "Antalya"},
                    {8, "Artvin"},
                    {9, "Ayd�n"},
                    {10, "Bal�kesir"},
                    {11, "Bilecik"},
                    {12, "Bing�l"},
                    {13, "Bitlis"},
                    {14, "Bolu"},
                    {15, "Burdur"},
                    {16, "Bursa"},
                    {17, "�anakkale"},
                    {18, "�ank�r�"},
                    {19, "�orum"},
                    {20, "Denizli"},
                    {21, "Diyarbak�r"},
                    {22, "Edirne"},
                    {23, "Elaz��"},
                    {24, "Erzincan"},
                    {25, "Erzurum"},
                    {26, "Eski�ehir"},
                    {27, "Gaziantep"},
                    {28, "Giresun"},
                    {29, "G�m��hane"},
                    {30, "Hakkari"},
                    {31, "Hatay"},
                    {32, "Isparta"},
                    {33, "Mersin"},
                    {34, "�stanbul"},
                    {35, "�zmir"},
                    {36, "Kars"},
                    {37, "Kastamonu"},
                    {38, "Kayseri"},
                    {39, "K�rklareli"},
                    {40, "K�r�ehir"},
                    {41, "Kocaeli"},
                    {42, "Konya"},
                    {43, "K�tahya"},
                    {44, "Malatya"},
                    {45, "Manisa"},
                    {46, "Kahramanmara�"},
                    {47, "Mardin"},
                    {48, "Mu�la"},
                    {49, "Mu�"},
                    {50, "Nev�ehir"},
                    {51, "Ni�de"},
                    {52, "Ordu"},
                    {53, "Rize"},
                    {54, "Sakarya"},
                    {55, "Samsun"},
                    {56, "Siirt"},
                    {57, "Sinop"},
                    {58, "Sivas"},
                    {59, "Tekirda�"},
                    {60, "Tokat"},
                    {61, "Trabzon"},
                    {62, "Tunceli"},
                    {63, "�anl�urfa"},
                    {64, "U�ak"},
                    {65, "Van"},
                    {66, "Yozgat"},
                    {67, "Zonguldak"},
                    {68, "Aksaray"},
                    {69, "Bayburt"},
                    {70, "Karaman"},
                    {71, "K�r�kkale"},
                    {72, "Batman"},
                    {73, "��rnak"},
                    {74, "Bart�n"},
                    {75, "Ardahan"},
                    {76, "I�d�r"},
                    {77, "Yalova"},
                    {78, "Karab�k"},
                    {79, "Kilis"},
                    {80, "Osmaniye"},
                    {81, "D�zce"}
                };
        }
    }
}
