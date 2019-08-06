using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_giriscikis
{
    //_kayitlar.txt dokümanına veri işleme
    class Yaz
    {
        //yapıcı metotota yazma işlem gerçekleştirme
        public Yaz(string dosya_yolu, DateTime tarih, string metin_adi, string metin_icerik)
        {

            try
            {


                //readalllines dodyadaki tüm satırları okur,length özeliğiyle uzunlugunu aldık
                int satir_sayisi = File.ReadAllLines(dosya_yolu).Length + 1;

                FileStream yz = new FileStream(dosya_yolu, FileMode.Append);

                //connecetion open ve close  yapısını barıdırıdır
                //StreamWriter -yazma bufferı oluştururr sadece yazma işemi yapar 

                using (StreamWriter sw = new StreamWriter(yz))
                {
                    sw.Write(satir_sayisi + " ");
                    sw.Write(tarih + " ");
                    sw.Write(metin_adi + " ");
                    sw.WriteLine(metin_icerik + " ");

                }


            }
            catch (Exception hata)
            {
                Console.WriteLine("Hata:" + hata.Message);
            }


        }

        //~Yaz()
        //{
        //    Console.WriteLine("İşlem tamamlandı");
        //}

    }

    //kayıt listeleme
    class Listele
    {
        public void Liste(string dosya_yolu)
        {

            // okuma işlemi yapılacak olan dosya yoluna buffet yanı tüel oluşturup yazma işelm yapılacak baglantı açılıyor
            using (StreamReader sr = new StreamReader(dosya_yolu))
            {
                //dosya yolunda bulunan çalışmanın tamamını string dokuman değişkenine yükledik
                string dokuman = sr.ReadToEnd().ToString();
                Console.WriteLine(dokuman);
            } //bağlantı kapatılıyor



        }

    }

    //kayıt güncellme
    class Guncelle
    {

        public Guncelle(string dosya_yolu)
        {
            //dosya yolunda bulunan tüm verileri bl nesnesinde tutuyoruz
            Listele bl = new Listele();
            bl.Liste(dosya_yolu);
            int _dStr = 0;
            string _baslik = " ";
            string _metin = " ";

            try
            {


                //burada ReadAllLines -Tüm Satıtı al işe bunu listeye satır işlme
                var dosya = new List<string>(File.ReadAllLines(dosya_yolu));

                Console.WriteLine("Güncellemek istediğiniz satır numarını giriniz");
                int _strNO = int.Parse(Console.ReadLine());
                _dStr = _strNO;

                //satır sileme durumu veirilen indexe göre
                dosya.RemoveAt(_strNO - 1);

                //Tüm satısı yaz içinden removeatle silinmiiş hariç, vede toArray() metodu ile 
                //Bir dizi haline getir
                File.WriteAllLines(dosya_yolu, dosya.ToArray());
            }
            catch (Exception ht)
            {

                Console.WriteLine("Hata:" + ht);
            }

            //sistem satiri alır
            DateTime trh = DateTime.Now;

            try
            {



                Console.WriteLine("Kayıt Tarihi:" + trh);
                Console.WriteLine("Başlık Giriniz");

                _baslik = Console.ReadLine();

                Console.WriteLine("Metni Yazınız");

                _metin = Console.ReadLine();


            }
            catch (ArgumentException)
            {
                Console.WriteLine("Arguman hatası");

            }
            catch (FormatException)
            {
                Console.WriteLine("Format hatası");
            }


            catch (NullReferenceException)
            {
                Console.WriteLine("Değer boş geçilemez hatası");
            }
            finally
            {

                try
                {
                    FileStream wd = new FileStream(dosya_yolu, FileMode.Append);

                    using (StreamWriter sw = new StreamWriter(wd))
                    {
                        sw.Write(_dStr + " ");
                        sw.Write(trh + " ");
                        sw.Write(_baslik + " ");
                        sw.WriteLine(_metin + " ");

                    }

                    Console.WriteLine("Güncelleme tamamlandı");

                }
                catch (Exception ex)
                {

                    Console.WriteLine("Hata:" + ex);

                }

            }


        }

    }


    class Sil
    {
        public Sil(string dosya_yolu)
        {

            //dosya yolunda bulunan tüm verileri bl nesnesinde tutuyoruz
            Listele bl = new Listele();
            //listele ekrana yaıyor
            bl.Liste(dosya_yolu);

            int _scm = 0;

            Console.WriteLine("Silemek istediğiniz satır indexini giriniz");
            try
            {
                _scm = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {

                Console.WriteLine("Girişte hata:" + ex.Message);
            }

            try
            {
                var dosya = new List<string>(File.ReadAllLines(dosya_yolu));

                //ilgili seçilen satır removeat metoduyla liste silınıyor
                dosya.RemoveAt(_scm - 1);

                //oluşturalan yeni liste yapısı dosyaya akatarılıyor
                File.WriteAllLines(dosya_yolu, dosya.ToArray());
                Console.WriteLine("Silme İşlemi tamamlanldı");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata:" + ex.Message);
            }








        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            //connectionString 
            string dosya_yolu = @"C:\Users\HP\documents\visual studio 2017\Projects\oop-konular\IO-giriscikis\_kayitlar.txt";


            string scm;
            while (true)
            {
                #region Menu-seçim


                Console.Clear();
                Console.WriteLine("***************************");
                Console.WriteLine("*---Günlük Programı------**");
                Console.WriteLine("*1.Kayıt Ekleme ---------**");
                Console.WriteLine("*2.Kayıt Listeleme-------**");
                Console.WriteLine("*3.Kayıt Güncelleme -----**");
                Console.WriteLine("*4.Kayıt Silme-----------**");
                Console.WriteLine("*5.Çıkış ----------------**");

                Console.Write("Seçiminiz:");
                int secim = Convert.ToInt32(Console.ReadLine());
                #endregion


                switch (secim)
                {
                    case 1:
                        DateTime trh = DateTime.Now;

                        Console.WriteLine("Kayıt Tarihi:" + trh);
                        Console.Write("Başlık giriniz:");
                        string _baslik = Console.ReadLine();

                        Console.WriteLine("Metni Gririniz");
                        string _icerik = Console.ReadLine();

                        Yaz _yz = new Yaz(dosya_yolu, trh, _baslik, _icerik);
                        Console.WriteLine("İşlem tamamlandı");
                        break;
                    case 2:
                        Console.WriteLine("Günlükteki Kayıtlarınız");
                        Console.WriteLine("No------Tarih--------Başlık ----Metin-------");
                        Listele _lste = new Listele();
                        _lste.Liste(dosya_yolu);


                        break;
                    case 3:
                        Guncelle _gnclle = new Guncelle(dosya_yolu);


                        break;
                    case 4:

                        Sil _sl = new Sil(dosya_yolu);

                        break;
                    case 5:
                        System.Environment.Exit(0);
                        break;

                }



                #region Çıkış ve devam kontolü



                Console.WriteLine("İşleme devam etmek istiyormusunuz E/H");
                scm = Console.ReadLine().ToLower();

                // while döngüsünü terk etme devam et
                if (scm == "e")
                {
                    continue;
                }
                else break;
                #endregion

            }


        }
    }
}
