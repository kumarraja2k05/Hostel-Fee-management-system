namespace HostelFee{

    public class Result{

        public static void showResult(){
            try{
                string line = "";
                using (StreamReader sr = new StreamReader("output display//result.txt")) {
                    while ((line = sr.ReadLine()!) != null) {
                        Console.WriteLine(line);
                    }
                }
            }catch(Exception e){
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            
            Console.ReadKey();
        }
    }
}