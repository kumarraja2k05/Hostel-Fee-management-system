using System;

namespace HostelFee
{
    class HostelFeeManagementSystem{

        int total_hostel_fee;
        bool is_enrolled=false;
        string student_name;
        string hostel_name;
        int balance=0;
        int deposit_amount;
        public HostelFeeManagementSystem(int hostel_fee,string student_name,string hostel_name){
            this.total_hostel_fee=hostel_fee;
            this.student_name=student_name;
            this.hostel_name=hostel_name;
            this.deposit_amount=0;
        }
        public bool askStudentChoice(){
            // Ask student whether he wants to login or not
            Console.WriteLine("\nDo you want to enroll for hostel");
            Console.WriteLine("--Press 1 to Continue--");
            Console.WriteLine("--Press 2 if want to exist--");
            try{
                int choice=Convert.ToInt32(Console.ReadLine());
                if(choice.Equals(1)){
                    is_enrolled=true; 
                }else if(choice.Equals(2)){
                    is_enrolled=false;
                }
                else{
                    Console.WriteLine("Wrong Input!!");
                    askStudentChoice();
                }
            }catch(Exception){
                Console.WriteLine("Please give Integer input!!");
                askStudentChoice();
            }
            return is_enrolled;
        }

        public bool isEnrolled(){
            if(is_enrolled){
                Console.WriteLine($"{this.student_name} is enrolled in {this.hostel_name}");
            }else{
                Console.WriteLine($"Sorry student not Enrolled!!");
            }
            return is_enrolled;
        }
        public void depositMoney(int deposit_amount){
            // add money if want to opt for hostel

            if(is_enrolled){
                this.deposit_amount=deposit_amount;
                if(this.deposit_amount>(int)(0.6*this.total_hostel_fee)){
                    this.balance=this.total_hostel_fee-this.deposit_amount;
                    Console.WriteLine($"\nAmount of {deposit_amount} successfully Deposited!!\n");
                }else{
                    Console.WriteLine($"Please give atleast 60% of {this.total_hostel_fee} i.e {0.6*this.total_hostel_fee} rupees");
                }
                
            }else{
                Console.WriteLine("Sorry student not Enrolled!!");
            }
        }

        public void continueHostel(){
            // check want to continue hostel if yes than ask him to pay money for next sem
            // If not want to continue than check balance
            if(is_enrolled){
                Console.WriteLine("\nDo you want to continue Hostel?\n");
                Console.WriteLine("Press 1 for Continue\nPress 2 for Deallocation from Hostel");
                try{
                    int continue_hostel=Convert.ToInt32(Console.ReadLine());
                    if(continue_hostel==1){
                        is_enrolled=true;
                        Console.WriteLine($"--Thank You for Continuing Hostel {this.student_name}--");
                        // make changes here
                        if(lessFee()){
                            lateFeeCharge();
                            depositMoney(this.balance);
                        }else{
                            adjustFee();
                        }
                    }else if(continue_hostel==2){
                        if(lessFee()){
                            Console.WriteLine("Please clear your pending dues!!");
                        }else{
                            is_enrolled=false;
                            Console.WriteLine($"{this.student_name} is Deallocated Succesfully from {this.hostel_name}");
                        }
                        
                    }else{
                        Console.WriteLine("Wrong Input!!");
                        continueHostel();
                    }
                }catch{
                    Console.WriteLine("Please give Integer input!!");
                    continueHostel();
                }
            }else{
                Console.WriteLine($"{this.student_name} you are not in any Hostel!!");
            }
        }

        public int currentBal(){
            // check current balance
            return this.deposit_amount;
        }

        public void lateFeeCharge(){
            if(lessFee() && is_enrolled){
                Console.WriteLine("\nHostel Fee Required for this Semester: "+this.total_hostel_fee);
                int charge=(int)(0.05*total_hostel_fee);
                this.balance+=charge;
                Console.WriteLine($"Late Fee charge is Rupees {charge}");
            }else{
                Console.WriteLine("You donot have any late fee!!");
            }
        }
        public bool lessFee(){
            // if current balance less than actual amount than ask for late fees
            if(this.balance<this.total_hostel_fee){
                return true;
            }else{
                return false;
            }
        }

        public void adjustFee(){
            // if current balance greater than current amount than adjust fee for next semester
            if((!lessFee()) || this.balance<0){
                Console.WriteLine($"Fee of {this.balance} Adjusted in next Semester!!");
            }
        }

        public int user_options(){
            Console.WriteLine("\n***************************************************************\n");
            Console.WriteLine("Press 0 to ask student to enroll");
            Console.WriteLine("Press 1 to check if student is Enrolled in Hostel or not!!");
            Console.WriteLine("Press 2 to pay Hostel Fee");
            Console.WriteLine("Press 3 to check deposited Hostel Fee");
            Console.WriteLine("Press 4 if you want to continue Hostel");
            Console.WriteLine("Press 5 to check if you have Late fee charge");
            Console.WriteLine("Press 6 to Exit()");
            Console.WriteLine("\nEnter input: ");
            int choice;
            bool parsedSuccessfully = int.TryParse(Console.ReadLine(), out choice);
            if (parsedSuccessfully == false)
            {
                Console.WriteLine("Please type an Integer!");
                user_options();
            }
            return choice;
        }

    }
    class MainClass{
        public static void Main(string []args){
            Console.WriteLine("\n---Welcome To Hostel Fee Management System---\n");
            Console.WriteLine("Enter Name of Student: ");
            string student_name=Console.ReadLine();
            Console.WriteLine("\nEnter Hostel Name: ");
            string hostel_name=Console.ReadLine();
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

            // HostelFeeManagementSystem hostel_fee=new HostelFeeManagementSystem(total_hostel_fee,"Kumar Raja","Zakir-B");
            HostelFeeManagementSystem hostel_fee=new HostelFeeManagementSystem(total_hostel_fee,student_name,hostel_name);
            bool flag=true;
            while(flag){
                switch(hostel_fee.user_options()){
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
                        flag=false;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice!!");
                        hostel_fee.user_options();
                        break;
                }
            }
            
            Console.WriteLine("\n--Thank You For Using Hostel Fee Management System--\n");
            
        }
    }
}