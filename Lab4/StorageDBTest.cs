using System;
using Xunit;
using IIG.CoSFE.DatabaseUtils;
using IIG.Core.FileWorkingUtils;
using System.Text;

namespace Lab4
{
    public class StorageBDTest
    {
        private const string Server = @"DESKTOP-6V7SVAP\OTCHEBUCH";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"gabartem";
        private const int ConnectionTime = 75;
        private StorageDatabaseUtils storageDatabaseUtils = new StorageDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);

        [Fact]
        public void AddFileTest()
        {
            Assert.True(storageDatabaseUtils.AddFile("testfile.txt", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testfile.txt"))));
            Assert.True(storageDatabaseUtils.AddFile("db.accdb", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\db.accdb"))));
            Assert.True(storageDatabaseUtils.AddFile("docfile.docx", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\docfile.docx"))));
            Assert.True(storageDatabaseUtils.AddFile("pdftest.pdf", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\pdftest.pdf"))));
            Assert.True(storageDatabaseUtils.AddFile("archive.zip", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\archive.zip"))));
            Assert.True(storageDatabaseUtils.AddFile("picture.jpg", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\picture.jpg"))));
            Assert.True(storageDatabaseUtils.AddFile("present.pptx", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\present.pptx"))));
            Assert.True(storageDatabaseUtils.AddFile("✔️✔️✔️.txt", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\✔️✔️✔️.txt"))));
            Assert.True(storageDatabaseUtils.AddFile("ґєїъэё.txt", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\ґєїъэё.txt"))));
            Assert.True(storageDatabaseUtils.AddFile("mathcad.xmcd", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\mathcad.xmcd"))));
            Assert.True(storageDatabaseUtils.AddFile("somepng.png", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\somepng.png"))));
            Assert.True(storageDatabaseUtils.AddFile("Відео-файл.mp4", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\Відео-файл.mp4"))));
            Assert.True(storageDatabaseUtils.AddFile("music.mp3", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\music.mp3"))));
            Assert.True(storageDatabaseUtils.AddFile("apktest.apk", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\apktest.apk"))));
            Assert.True(storageDatabaseUtils.AddFile("someexe.exe", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\someexe.exe"))));
            Assert.True(storageDatabaseUtils.AddFile("HUH", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\HUH"))));
            Assert.True(storageDatabaseUtils.AddFile("oops.torrent", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\oops.torrent"))));
            Assert.True(storageDatabaseUtils.AddFile(".txt", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\.txt"))));
        }


        [Fact]
        public void AddFileWithEMptyName()
        {
            Assert.False(storageDatabaseUtils.AddFile("", Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testfile.txt"))));
        }


        [Fact]
        public void AddFileWithNullContent()
        {
            Assert.False(storageDatabaseUtils.AddFile("null.txt", null));
        }


        [Fact]
        public void AddFileWithEmptyContent()
        {
            Assert.True(storageDatabaseUtils.AddFile("empty.txt", new byte[0]));
        }


        [Fact]
        public void GetFileTrue()
        {
            string name;
            byte[] content;
            Assert.True(storageDatabaseUtils.GetFile(670, out name, out content));
        }


        [Fact]
        public void GetFileTest()
        {
            string name;
            byte[] content;
            Assert.True(storageDatabaseUtils.GetFile(627, out name, out content));
            Assert.Equal("testfile.txt", name);
            Assert.Equal(Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\testfile.txt")), content);
            storageDatabaseUtils.GetFile(628, out name, out content);
            Assert.Equal("db.accdb", name);
            Assert.Equal(Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\db.accdb")), content);
            storageDatabaseUtils.GetFile(634, out name, out content);
            Assert.Equal("✔️✔️✔️.txt", name);
            Assert.Equal(Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\✔️✔️✔️.txt")), content);
            storageDatabaseUtils.GetFile(635, out name, out content);
            Assert.Equal("ґєїъэё.txt", name);
            Assert.Equal(Encoding.ASCII.GetBytes(FileWorker.GetFileName(@"C:\testing\ґєїъэё.txt")), content);
        }


        [Fact]
        public void GetWrongFile()
        {
            Assert.False(storageDatabaseUtils.GetFile(0, out _, out _));
            Assert.False(storageDatabaseUtils.GetFile(25, out _, out _));
            Assert.False(storageDatabaseUtils.GetFile(-1, out _, out _));
        }


        [Fact]
        public void GetFilesTest()
        {
            Assert.NotNull(storageDatabaseUtils.GetFiles("testfile.txt"));
        }


        [Fact]
        public void DeleteFileTest()
        {
            Assert.True(storageDatabaseUtils.DeleteFile(0));
            Assert.True(storageDatabaseUtils.DeleteFile(-1));
            Assert.True(storageDatabaseUtils.DeleteFile(266));
            Assert.True(storageDatabaseUtils.DeleteFile(267));
            Assert.True(storageDatabaseUtils.DeleteFile(268));
            Assert.True(storageDatabaseUtils.DeleteFile(269));
        }
    }
}
