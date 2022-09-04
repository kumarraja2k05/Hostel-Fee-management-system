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
                double amount=0.6*this.total_hostel_fee;
                if(deposit_amount>(int)amount){
                    this.deposit_amount=deposit_amount;
                    this.balance=this.total_hostel_fee-this.deposit_amount;
                    if(deposit_amount>this.total_hostel_fee){
                        Console.WriteLine("You have paid extra amount!!");
                        adjustFee();
                    }
                    Console.WriteLine($"\nAmount of {deposit_amount} successfully Deposited!!\n");
                }
                else{
                    Console.WriteLine($"Please give atleast 60% of {this.total_hostel_fee} i.e {(int)amount} rupees");
                }
                
            }else{
                Console.WriteLine("Sorry student not Enrolled!!");
            }
        }

        public void remainingFee(){
            if(lessFee() || is_enrolled){
                if(this.balance<0){
                    adjustFee();
                }else{
                    Console.WriteLine("Remaining Fees is: "+this.balance+" Rupees");
                }
            }else{
                Console.WriteLine("Remaining Fees is: "+0+" Rupees");
                return;
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
                            Console.WriteLine($"\nPlease clear pending dues than only you will be Reallocated in hostel {this.hostel_name}");
                            lateFeeCharge();
                            depositMoney(this.balance+this.total_hostel_fee);
                        }else{
                            adjustFee();
                            while(true){
                                Console.WriteLine("\nDo You want to deposit Now y or no\n");
                                string choice=Console.ReadLine().ToLower();
                                if(choice.Equals("y")){
                                    depositMoney(this.total_hostel_fee+this.balance);
                                    break;
                                }else if(choice.Equals("n")){
                                    Console.WriteLine("You Chose not to Deposit Hostel Fee right now!");
                                    break;
                                }else{
                                    Console.WriteLine("--Please choose between y or n--");
                                }
                            }
                            
                        }
                    }else if(continue_hostel==2){
                        if(lessFee()){
                            Console.WriteLine("Please clear your pending dues!!");
                            lateFeeCharge();
                            depositMoney(this.balance);
                        }else{
                            is_enrolled=false;
                            adjustFee();
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
                Console.WriteLine($"You have deposited {currentBal()} rupees only");
                int charge=(int)(0.05*total_hostel_fee);
                this.balance+=charge;
                Console.WriteLine($"Late Fee charge is Rupees {charge}");
            }else{
                Console.WriteLine("You donot have any late fee!!");
            }
        }
        public bool lessFee(){
            // if current balance less than actual amount than ask for late fees
            if(this.deposit_amount<this.total_hostel_fee){
                return true;
            }else{
                return false;
            }
        }

        public void adjustFee(){
            // if current balance greater than current amount than adjust fee for next semester
            if((!lessFee()) || this.balance<0){
                Console.WriteLine($"Fee of {Math.Abs(this.balance)} Adjusted in next Semester!!");
            }
        }

        public void user_options(){
            Console.WriteLine("\n***************************************************************\n");
            Console.WriteLine("Press 0 to ask student to enroll");
            Console.WriteLine("Press 1 to check if student is Enrolled in Hostel or not!!");
            Console.WriteLine("Press 2 to pay Hostel Fee");
            Console.WriteLine("Press 3 to check deposited Hostel Fee");
            Console.WriteLine("Press 4 if you want to continue Hostel");
            Console.WriteLine("Press 5 to check if you have Late fee charge");
            Console.WriteLine("Press 6 to check remaining Fee if any");
            Console.WriteLine("Press 7 to display student details");
            Console.WriteLine("Press 8 to Exit()");
            Console.WriteLine("\nEnter input: ");
        }

        public void display(){
            Console.WriteLine("\n*********************************************************************\n");
            Console.WriteLine("--Student Details--");
            Console.WriteLine($"Student Name: {this.student_name}");
            Console.WriteLine($"Hostel name: {this.hostel_name}");
            Console.WriteLine($"Deposited Amount: {this.deposit_amount}");
            int val=this.balance<0 ? 0 : this.balance;
            Console.WriteLine($"Remaining Balance: {val}");
            
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