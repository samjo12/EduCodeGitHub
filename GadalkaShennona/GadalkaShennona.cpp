#include <iostream>
#include <conio.h>
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <time.h>
#include <stdlib.h>

#define SCANCODE_ONE 49
#define SCANCODE_ZERO 48

typedef unsigned short us;
typedef unsigned char  uc;
typedef unsigned long  ul;

typedef struct
{
    ul zero;
    ul one;
} datatype;


// запрос символа с клавы
uc getnum(void)
{
    uc j, k = 0;
    //Закомментируй строки ниже, чтобы прога играла сама с собой
    /**/while ( !_kbhit() );
      while ( !k )
         k = _getch();
      j=255;
      if (k == SCANCODE_ONE)j = 1; else
          if (k == SCANCODE_ZERO)j = 0;/**/
      /*расскоменть строку ниже, чтобы прога играла сама с собой*/
      //int i = rand() % 2; return i;
    return j;
}


void updatedata(datatype** data, uc* ans, uc depth)
{
    int i, k;
    ul j;
    for (i = (depth - 1); i >= 0; i--) {
        j = 0;
        for (k = (depth - 1); k >= i; k--)
            j += ans[k] * (1 << (depth - 1 - k));
        if (ans[depth] == 1)
            data[depth - 1 - i][j].one++;
        if (ans[depth] == 0)
            data[depth - 1 - i][j].zero++;
    }
}


uc analyse(datatype** data, uc* ans, uc depth)
{
    int i, k;
    ul j, zero = 0, one = 0;

    for (i = (depth - 1); i >= 0; i--) {
        j = 0;
        for (k = (depth - 1); k >= i; k--)
            j += ans[k] * (1 << (depth - 1 - k));
        one += data[depth - 1 - i][j].one;
        zero += data[depth - 1 - i][j].zero;
    }
    if (zero > one)
        return 0;
    else
        return 1;
}


int main(void)
{
    srand((unsigned)time(NULL));
    ul all_answer = 0, wrong_answer = 0;
    int depth;
    datatype** data;
    uc* ans;
    us i;
    uc t_ans, c;
    

    printf("\nDepth (1..20) : "); 
    std::cin >> depth;

    ans = (uc*)calloc(depth + 1, sizeof(uc*));
    if (ans == NULL) exit(1);

    if (((data = (datatype**)malloc(depth * sizeof(datatype*))) == NULL)) {
        puts("Not enough memory!");
        exit(1);
    }

    for (i = 0; i < depth; i++)
        if ((data[i] = (datatype*)calloc((1 + (1 << (i + 1))), sizeof(datatype))) == NULL) {
            puts("Not enough memory!");
            exit(1);
        }

    printf("\nPress '0' or '1'.");
    printf("\nAny other key - exit.\n\n");
    int xy = 0;
    while (1) { //включаем гадалку
        for (i = 0; i < depth; i++)
            ans[i] = ans[i + 1];
        t_ans = analyse(data, ans, depth);

        if ((ans[depth] = getnum()) == 255)
            return 0;

        updatedata(data, ans, depth);
        all_answer++;
        
        c = '+';
        if (t_ans != ans[depth]) {
            wrong_answer++;
            c = '-';
        }
        xy = xy + 1;
        if ((xy % 1000000) != 0) 
                  printf("%d - %d ? %0.3f ? %c ? %d ?\n", ans[depth], t_ans, (all_answer - wrong_answer) / (all_answer + 1.0), c, all_answer);
        
    }
}


