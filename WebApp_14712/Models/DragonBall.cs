namespace WebApp_14712.Models
{

    // public class DragonBallList

    // {
    //     public List<DragonBallApiResponse> item { get; set; }

    // }
    
    public class DragonBallApiResponse

    {
        public string name { get; set; }
        public ImgUrl imgUrl { get; set; }
    }

    public class ImgUrl
    {
        public string png { get; set; }
    }

    public class DragonBall
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
    }


}