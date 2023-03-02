
using BlackDigital.DataAnnotations;
using BlackDigital.Test.Mock;
using System.Net;

namespace BlackDigital.Test.DataAnnotions
{
    public class ShowIfAttributeTest
    {
        [Fact]
        public void ShowAttribute()
        {
            ShowIfPropertyAttribute showIfAttributte = new("HttpStatus", HttpStatusCode.OK, 
                                                                 HttpStatusCode.Created, 
                                                                 HttpStatusCode.Accepted,
            HttpStatusCode.NoContent);

            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.OK }));
            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Created }));
            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Accepted }));
            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.NoContent }));

            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.BadRequest }));
            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Conflict }));
            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Gone }));
            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.InternalServerError }));
        }

        [Fact]
        public void ShowAttributeInverted()
        {
            ShowIfPropertyAttribute showIfAttributte = new("HttpStatus", HttpStatusCode.OK,
                                                                 HttpStatusCode.Created,
                                                                 HttpStatusCode.Accepted,
                                                                 HttpStatusCode.NoContent)
            {
                IsInverted = true
            };

            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.OK }));
            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Created }));
            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Accepted }));
            Assert.False(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.NoContent }));

            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.BadRequest }));
            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Conflict }));
            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.Gone }));
            Assert.True(showIfAttributte.Show(new SimpleModel() { HttpStatus = HttpStatusCode.InternalServerError }));
        }
    }
}
