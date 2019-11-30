using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAD.Model;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.DbContext
{
    public interface IDatabaseInitializer
    {
        Task MigrateAsync();
        Task SeedAsync();
    }
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DatabaseInitializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    Email = "admin@ad.pl",
                    UserName = "Administrator"
                };
                await _userManager.CreateAsync(user, "1qaz@WSX");
            }
            try
            {

                if (!await _context.Rooms.AnyAsync())
                {
                    await SeedRoomsAsync();
                }

                if (!await _context.Rooms.AnyAsync())
                {
                    await SeedCardsAsync();
                    await SeedAllowedRoomsAsync();
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                ;
            }
        }

        private async Task SeedAllowedRoomsAsync()
        {
            using (StreamReader reader = new StreamReader("InitialData\\allowedRooms.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var text = await reader.ReadLineAsync();
                    var splittedText = text.Split(';');
                    var cardRoom = new CardRoom()
                    {
                        Card = _context.Cards.Local.FirstOrDefault(card => card.SerialNumber.Equals(splittedText[0])),
                        Room = _context.Rooms.Local.FirstOrDefault(card => card.Number.Equals(splittedText[1]))
                    };
                    try
                    {
                        await _context.CardRoom.AddAsync(cardRoom);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        public async Task SeedRoomsAsync()
        {
            using (StreamReader reader = new StreamReader("InitialData\\rooms.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var text = await reader.ReadLineAsync();
                    var splittedText = text.Split(';');
                    var room = new Room()
                    {
                        Number = splittedText[0],
                        Floor = int.Parse(splittedText[1])
                    };
                    await _context.Rooms.AddAsync(room);
                }
            }
        }

        public async Task SeedCardsAsync()
        {
            using (StreamReader reader = new StreamReader("InitialData\\cards.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var text = await reader.ReadLineAsync();
                    var splittedText = text.Split(';');
                    var card = new Card()
                    {
                        SerialNumber = splittedText[0],
                        User = _context.Users.FirstOrDefault(user => user.Email.Equals(splittedText[1]))
                    };
                    await _context.Cards.AddAsync(card);

                };
            }
        }
    }
}

