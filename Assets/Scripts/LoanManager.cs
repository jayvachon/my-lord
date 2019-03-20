using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoanManager
{
    static bool hasLoan = false;
    static Loan loan;

    public static Loan NewLoan() {
    	if (!hasLoan) {
	    	loan = new Loan(5000000, 0.005f, 10);
	    	return loan;
    	} else {
    		return null;
    	}
    }

    public static Loan GetLoan() {
    	return loan;
    }
}
