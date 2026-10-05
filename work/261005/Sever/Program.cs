var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var itemList = new List<Item>()
{
	new Item(1, "apple", 200),
	new Item(2, "game", 5000),
	new Item(3, "beer", 400)
};

app.MapGet("/items", () => 
{
	return Results.Ok(itemList);
});

app.MapGet("/items/{id}", (int id) =>
{
	int count = itemList.Count();
	for (int i=0; i<count; i++)
	{
		if (itemList[i].id == id)
		{
			return Results.Ok(itemList[i]);
		}
	}
	return Results.NotFound();
});

app.MapGet("/slow", async (int ms) =>
{
	if ((ms < 0) || (ms >= 10000))
	{
		return Results.BadRequest(new { message = "ŽžŠÔ‚Í0ˆÈã10000–¢–ž‚ÅŽw’è‚µ‚Ä‚­‚¾‚³‚¢B" });
	}

	await Task.Delay(ms);
	return Results.Ok(new { ms = ms });
});

app.MapGet("/error", () =>
{
	return Results.StatusCode(500);
});

app.Run();

public record Item(int id, string Name, int Price);