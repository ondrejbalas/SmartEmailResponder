using System.Collections.Generic;
using SmartEmailResponder.Models;

namespace SmartEmailResponder.Data;

public static class FaqLoader
{
    public static IEnumerable<Faq> GetAllFaqs()
    {
        yield return new Faq("What are your hours?", "Our hours are Monday through Friday 6am to 6pm.");
        yield return new Faq("Do you have any gluten-free or vegan options?", "We offer a few gluten-free and vegan options.");
        yield return new Faq("What type of coffee beans do you use?", "We use 100% Arabica coffee beans.");
        yield return new Faq("How do you make your coffee?", "We make our coffee using a drip brewing method.");
        yield return new Faq("Do you have any iced coffee options?", "We offer iced coffees.");
        yield return new Faq("What are your most popular drinks?", "Our most popular drinks include lattes.");
        yield return new Faq("Do you have any seasonal drinks?", "We offer a few seasonal drinks throughout the year.");
        yield return new Faq("Do you have any food options?", "We offer a few food options.");
        yield return new Faq("Do you offer catering?", "We do offer catering services for events.");
        yield return new Faq("Do you have a loyalty program?", "We do have a loyalty program where customers can earn points for every purchase that can be redeemed for future discounts.");
        yield return new Faq("Do you sell gift cards?", "We do sell gift cards that can be used at our store.");

    }
}