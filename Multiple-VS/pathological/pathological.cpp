// pathological.cpp 

#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>

/* Generates many traces by creating a pathologically bad dataflow graph.
 * Demonstrates a basic issue with IPVA trace representation (RT June 2012).
*/

/* Leaf input routine */
static char * getinput1 (bool b, char * buf)
{
	// Get some taint from user
	fgets(buf, 100, stdin);
	return buf;
}

/* Leaf input routine */
static char * bgetinput1 (bool b, char * buf)
{
	// Get some taint from the user
	fgets(buf, 100, stdin);
	return buf;
}

/* Calls one of the 2 leaf input routines that get tainted input,
 * depending on a boolean flag
 */
static char * getinput2 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput1(b, buf);
	else
	    i = bgetinput1(b, buf);
	// Phi function will merge 2 possible paths leading to 'i' being tainted
	return i;
}

/* Calls one of the 2 leaf input routines that get tainted input,
 * depending on a boolean flag
 */
static char * bgetinput2 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput1(b, buf);
	else
	    i = bgetinput1(b, buf);
    // Phi function will merge 2 possible paths leading to 'i' being tainted
	return i;
}

/* Calls one of two of the level-2 getinput routines, each of which call 1 of 2 of the leaf input routines. */
static char * getinput3 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput2(b, buf);
	else
	    i = bgetinput2(b, buf);
	// Phi function will merge 2 possible paths leading to 'i' being tainted.
	// Since each of those already merged 2 paths, the stt on i becomes width-4 here.
	return i;
}

static char * bgetinput3 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput2(b, buf);
	else
	    i = bgetinput2(b, buf);
	// Phi function will merge 2 possible paths leading to 'i' being tainted.
	// Since each of those already merged 2 paths, the stt on i becomes width 4 here.
	return i;
}

// Continuing this pattern causes width of traces on i to grow by a power of 2 on each level.

static char * getinput4 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput3(b, buf);
	else
	    i = bgetinput3(b, buf);
	return i;
}

static char * bgetinput4 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput3(b, buf);
	else
	    i = bgetinput3(b, buf);
	return i;
}

static char * getinput5 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput4(b, buf);
	else
	    i = bgetinput4(b, buf);
	return i;
}

static char * bgetinput5 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput4(b, buf);
	else
	    i = bgetinput4(b, buf);
	return i;
}

static char * getinput6 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput5(b, buf);
	else
	    i = bgetinput5(b, buf);
	return i;
}

static char * bgetinput6 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput5(b, buf);
	else
	    i = bgetinput5(b, buf);
	return i;
}

static char * getinput7 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput6(b, buf);
	else
	    i = bgetinput6(b, buf);
	return i;
}

static char * bgetinput7 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput6(b, buf);
	else
	    i = bgetinput6(b, buf);
	return i;
}

static char * getinput8 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput7(b, buf);
	else
	    i = bgetinput7(b, buf);
	return i;
}

static char * bgetinput8 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput7(b, buf);
	else
	    i = bgetinput7(b, buf);
	return i;
}

static char * getinput9 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput8(b, buf);
	else
	    i = bgetinput8(b, buf);
	return i;
}

static char * bgetinput9 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput8(b, buf);
	else
	    i = bgetinput8(b, buf);
	return i;
}

static char * getinput10 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput9(b, buf);
	else
	    i = bgetinput9(b, buf);
	return i;
}

static char * bgetinput10 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput9(b, buf);
	else
	    i = bgetinput9(b, buf);
	return i;
}

static char * getinput11 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput10(b, buf);
	else
	    i = bgetinput10(b, buf);
	return i;
}

static char * bgetinput11 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput10(b, buf);
	else
	    i = bgetinput10(b, buf);
	return i;
}

static char * getinput12 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput11(b, buf);
	else
	    i = bgetinput11(b, buf);
	return i;
}

static char * bgetinput12 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput11(b, buf);
	else
	    i = bgetinput11(b, buf);
	return i;
}

static char * getinput13 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput12(b, buf);
	else
	    i = bgetinput12(b, buf);
	return i;
}

static char * bgetinput13 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput12(b, buf);
	else
	    i = bgetinput12(b, buf);
	return i;
}

static char * getinput14 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput13(b, buf);
	else
	    i = bgetinput13(b, buf);
	return i;
}

static char * bgetinput14 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput13(b, buf);
	else
	    i = bgetinput13(b, buf);
	return i;
}

static char * getinput15 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput14(b, buf);
	else
	    i = bgetinput14(b, buf);
	return i;
}

static char * bgetinput15 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput14(b, buf);
	else
	    i = bgetinput14(b, buf);
	return i;
}

static char * getinput16 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput15(b, buf);
	else
	    i = bgetinput15(b, buf);
	return i;
}

static char * bgetinput16 (bool b, char * buf)
{
	char * i;
	if (b)
		i = getinput15(b, buf);
	else
	    i = bgetinput15(b, buf);
	return i;
}

// End of input routines

/* Leaf (sink). Print the taint. */
static void print1(char buf[])
{
	printf("Input string was: %s\n", buf);
}

/* Leaf (sink). Print the taint */
static void bprint1(char buf[])
{
	printf("Input string was: %s\n", buf);
}

/* Level 2 output routine - calls each of 2 leaf routines */
static void print2(char buf[])
{
	print1(buf);
	bprint1(buf);
}

/* Level 2 output routine - calls each of 2 leaf routines */
static void bprint2(char buf[])
{
	print1(buf);
	bprint1(buf);
}

// Continuing this pattern, n levels of print routines cause dynamic callgraph
// to grow to 2**n in size on the way down

static void print3(char buf[])
{
	print2(buf);
	bprint2(buf);
}

static void bprint3(char buf[])
{
	print2(buf);
	bprint2(buf);
}

static void print4(char buf[])
{
	print3(buf);
	bprint3(buf);
}

static void bprint4(char buf[])
{
	print3(buf);
	bprint3(buf);
}

static void print5(char buf[])
{
	
	print4(buf);
	bprint4(buf);
}

static void bprint5(char buf[])
{
	print4(buf);
	bprint4(buf);
}

static void print6(char buf[])
{
	print5(buf);
	bprint5(buf);
}

static void bprint6(char buf[])
{
	print5(buf);
	bprint5(buf);
}

static void print7(char buf[])
{
	print6(buf);
	bprint6(buf);
}

static void bprint7(char buf[])
{
	print6(buf);
	bprint6(buf);
}

static void print8(char buf[])
{
	print7(buf);
	bprint7(buf);
}

static void bprint8(char buf[])
{
	print7(buf);
	bprint7(buf);
}

static void print9(char buf[])
{
	print8(buf);
	bprint8(buf);
}

static void bprint9(char buf[])
{
	print8(buf);
	bprint8(buf);
}

static void print10(char buf[])
{
	print9(buf);
	bprint9(buf);
}

static void bprint10(char buf[])
{
	print9(buf);
	bprint9(buf);
}

static void print11(char buf[])
{
	print10(buf);
	bprint10(buf);
}

static void bprint11(char buf[])
{
	print10(buf);
	bprint10(buf);
}

static void print12(char buf[])
{
	print11(buf);
	bprint11(buf);
}

static void bprint12(char buf[])
{
	print11(buf);
	bprint11(buf);
}

static void print13(char buf[])
{
	print12(buf);
	bprint12(buf);
}

static void bprint13(char buf[])
{
	print12(buf);
	bprint12(buf);
}

static void print14(char buf[])
{
	print13(buf);
	bprint13(buf);
}

static void bprint14(char buf[])
{
	print13(buf);
	bprint13(buf);
}

static void print15(char buf[])
{
	print14(buf);
	bprint14(buf);
}

static void bprint15(char buf[])
{
	print14(buf);
	bprint14(buf);
}

static void print16(char buf[])
{
	print15(buf);
	bprint15(buf);
}

static void bprint16(char buf[])
{
	print15(buf);
	bprint15(buf);
}

/* Main routine */
int _tmain(int argc, _TCHAR* argv[])
{
	char local_buf[100]; // this buffer will receive some taint
	char *s = local_buf;
	bool b = (rand() == 0);

	// We can vary the number of levels N by altering which getinputN / printN we call into.
	// The more levels, the more time & memory we use.
	// 
	// Measurements:
	//
	// Old trace representation (compute limit=0, prune heuristic=0)
	//==============================================================
	//  Levels    #findings    process-time    process-memory   assess-time  assess-memory           
	//    1         4             -                -               -             -   
	//    2         10            -                -               -             -
	//    3         38            -                -               -             -
	//    4         170           -                -               -             -
	//    5         786           -                -               -             -
	//    6         3618          0 sec           1032M            30 sec        1032M  
	//    7         16450         0 sec           1032M          4:18 min        1032M     
	//    8         73858         1 sec           1032M         38:08 min        1071M      
	//    9         327938        9 sec           1038M          5:55:18 hour    2532M      
	//   10           -          51 sec           1226N          ~25 hours       [clipped]      
	//   11           -        2:37 min           2196M            -               -           
	//   12           -        4:44 min           [clipped]        -               -           
	
    // New trace representation (compute limit=0, prune heuristic=0)
	//==============================================================
	//  Levels    #findings  process-time  process-memory assess-time assess-memory 
	//    1          4
	//    2          10
	//    3          38
	//    4          170
	//    5          786
	//    6          3618       0 sec            1032M            4 sec         1032M        
	//    7          16450      0 sec            1032M           34 sec         1032M         
	//    8          73858      0 sec            1032M         4:33 min         1105M       
	//    9          327938     0 sec            1032M        39:21 min         1921M      
	//   10          *          0 sec            1032M      11:07:11 hours     4264M & clipped  
	//   11          -          1 sec            1032M           -               -           
	//   12          -          7 sec            1032M           -               -         
	//   13          -         31 sec            1215M           -               -            
	//   14          -       2:50 min            2287M           -               -            
	//   15          -      12:39 min          [clipped]         -


	// Get tainted input in our buffer
	s = getinput7(b, local_buf);

	// Print the tainted buffer
	print7(s);

	return 0;
}

