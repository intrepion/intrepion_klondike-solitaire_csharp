using Bogus;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Intrepion.KlondikeSolitaire.AcceptanceTests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public partial class MoveAdminPages : PageTest
{
    [Test]
    public async Task MainNavigation()
    {
        var faker = new Faker();
        var aRandomString = faker.Random.String2(10);
        var someRandomString = faker.Random.String2(10);
        await Page.GetByTestId("moveNavLink").ClickAsync();
        await Expect(Page).ToHaveTitleAsync("Move Home");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync("Move Add");

        await Page.GetByLabel("From Pile Index:", new() { Exact = true }).FillAsync("1");
        await Page.GetByLabel("Move Time:", new() { Exact = true }).FillAsync("2011-01-01");
        await Page.GetByLabel("To Pile Index:", new() { Exact = true }).FillAsync("1");
        // CreatePropertyCodePlaceholder

        await Page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync("Move View");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Edit" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync("Move Edit");

        await Page.GetByLabel("From Pile Index:", new() { Exact = true }).FillAsync("2");
        await Page.GetByLabel("Move Time:", new() { Exact = true }).FillAsync("2022-02-02");
        await Page.GetByLabel("To Pile Index:", new() { Exact = true }).FillAsync("2");
        // ModifyPropertyCodePlaceholder

        await Page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync("Move View");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Delete" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirm" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Back to Grid" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync("Move Home");
    }

    [SetUp]
    public async Task SetUp()
    {
        var baseUrl = Environment.GetEnvironmentVariable("BASE_URL");
        if (string.IsNullOrEmpty(baseUrl))
        {
            baseUrl = "http://localhost:5121";
        }
        await Page.GotoAsync(baseUrl);


        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Expect(Page).ToHaveTitleAsync("Log in");
        await Page.GetByTestId("loginEmail").FillAsync("Admin1@Intrepion.KlondikeSolitaire.com");
        await Page.GetByTestId("loginPassword").FillAsync("Admin1@Intrepion.KlondikeSolitaire.com");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await Page.GetByRole(AriaRole.Button, new() { Name = "Logout" }).ClickAsync();
    }
}
