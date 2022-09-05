using System;
using HostelFee;

namespace HostelFee{
    class Animal{
        public static void show(){
            Console.WriteLine("Hello!!");
        }
    }

    class MainClass{
        public static void Main(string []args){
            Console.WriteLine("\n---Welcome To Hostel Fee Management System---\n");
            Console.WriteLine("Enter Name of Student: ");
            string student_name=Console.ReadLine()!;
            Console.WriteLine("\nEnter Hostel Name: ");
            string hostel_name=Console.ReadLine()!;
            int total_hostel_fee;
            while(true){
                Console.WriteLine("\nEnter the Hostel Fee Required for this Semester: ");
                try{
                    total_hostel_fee=Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nHostel Fee Required for this Semester: "+total_hostel_fee);
                    break;
                }catch{
                    Console.WriteLine("Please give Integer input!!");
                }
            }

            HostelFeeManagementSystem hostel_fee=new HostelFeeManagementSystem(total_hostel_fee,student_name,hostel_name);
            bool flag=true;
            while(flag){
                hostel_fee.user_options();
                try{
                    int user_choice=Convert.ToInt32(Console.ReadLine());
                    if(user_choice>=0 && user_choice<=8){
                        switch(user_choice){
                            case 0:
                                flag=hostel_fee.askStudentChoice();
                                break;
                            case 1:
                                hostel_fee.isEnrolled();
                                break;
                            case 2:
                                while(true){
                                    Console.WriteLine("\nHow Much money You want to deposit: ");
                                    try{
                                        int deposit_amount=Convert.ToInt32(Console.ReadLine());
                                        hostel_fee.depositMoney(deposit_amount);
                                        break;
                                    }catch{
                                        Console.WriteLine("Please give Integer input!!");
                                    }
                                }
                                break;
                            case 3:
                                Console.WriteLine("Current Balance is: "+hostel_fee.currentBal());
                                break;
                            case 4:
                                hostel_fee.continueHostel();
                                break;
                            case 5:
                                hostel_fee.lateFeeCharge();
                                break;
                            case 6:
                                hostel_fee.remainingFee();
                                break;
                            case 7:
                                hostel_fee.display();
                                break;
                            case 8:
                                flag=false;
                                break;
                            default:
                                Console.WriteLine("Invalid Choice!!");
                                hostel_fee.user_options();
                                break;
                        }
                    }else{
                        Console.WriteLine("Wrong Input!!");
                    }
                }catch{
                    Console.WriteLine("Please type an Integer!");
                }
                
            }
            
            Console.WriteLine("\n--Thank You For Using Hostel Fee Management System--\n");
            
        }
}
}