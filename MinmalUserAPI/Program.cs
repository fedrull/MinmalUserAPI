
using MinmalUserAPI.Data;
using MinmalUserAPI.Moduels;

namespace MinmalUserAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<DataContext>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			
			app.MapGet("/User", async (DataContext db) =>
			{
				return Results.Ok(await db.Users.ToListAsync());
			});

			app.MapGet("/User/{Id}", async (DataContext db,int Id) =>
			{
				var user = await db.Users.FindAsync(Id);
				if(user is null)
					return Results.NotFound("Not Found");
				db.SaveChanges();
				return Results.Ok(user);
			});


			app.MapPost("/User", async (Users User, DataContext db) =>
			{

				
				db.Users.Add(User);
				await db.SaveChangesAsync();
				return Results.Ok();
			});

			app.MapPut("/User/{Id}", async (Users userUpdate,int Id, DataContext db) =>
			{
				var user = db.Users.Update(userUpdate);
				if (user is null)
					return Results.NotFound("Not Found");
				
				return Results.Ok(user);
			});

			app.MapDelete("/User/{Id}", async (int Id, DataContext db) =>
			{
				var user = await db.Users.FindAsync(Id);
				if (user is null)
					return Results.NotFound("Not Found");

				db.Users.Remove(user);
				db.SaveChanges();
				return Results.Ok();
			});

			app.Run();
		}
	}
}