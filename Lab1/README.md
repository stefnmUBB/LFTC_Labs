# My Little Pascal


## 1. Specificarea

```
letter           = "a" | "b" | "c" | "d" | "e" | "f" | … | "z";
digit            = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9";
identifier       = letter, { letter|digit };
datatype         = "integer" | "real" | identifier;

number_literal   = ["+"|"-"], (int_literal  |  real_literal);
int_literal      = digits;
real_literal     = digits, [".", digits ];
digits           = digit, {digit};

arithm_expr_term = number_literal |  identifier |  ( "(", arithm_expr, ")" );

muldiv_expr      = arithm_expr_term, { ("*"|"/"|"div"|"mod"), arithm_expr_term };
addsub_expr      = muldiv_expr, { ("+"|"-"), muldiv_expr };
arithm_expr      = addsub_expr;

cond_expr        = arithm_expr, ("<"|"<="|">"|">="|"="|"<>"), arithm_expr;


vardecl_block    = "var", var_declaration, {";", var_declaration};
var_declaration  = identifier, ":", datatype;

instr_block      = "begin", [instr, {";", instr}], "end";

instr            = i_attr | i_cond | i_while;
compound_instr   = (instr | instr_block);

i_attr           = identifier, ":=", arithm_expr;
i_cond           = "if", "(", cond_expr, ")", "then", compound_instr,	            ["else", compound_instr];
i_while          = "while", cond_expr, "do", (instr | instr_block);
i_for            = "for", identifier, ":=", int_literal, ["to"|"downto"],int_literal, 
                    "do", compound_instr;

program          = "program", identifier, [vardecl_block], instr_block, "end", ".";
```

## 2. Exemple surse

-	 calculeaza perimetrul si aria cercului de o raza data data

```
program cerc;
var pi, r, p, a:real;
begin
    pi:=3.14;
    readln(r);
    p :=  2 * pi * r;
    a := pi *  r * r;
    writeln(p);
    writeln(a)
end.
```

-	determina cmmdc a 2 nr naturale

```
program cmmdc;
var a,b,r:integer;
begin
    readln(a,b);
    while (b<>0) do
    begin
        r := a mod b;
        a := b;
        b := r;
    end;
    writeln(a)
end.
```

-	calculeaza suma a n numere citite de la tastatura
```Pascal
program sum;
var n,i,x,s:integer;
begin
    s:=0;
    readln(n);
    for i:=1 to n do
    begin
        readln(x);
        s:=s+x;
    end;
    writeln(s)
end.
```




https://web.archive.org/web/20210928043025/http://pascal-central.com/iso7185.html

## 3. Surse care nu compileaza

-	nici in MLP nici in limbajul original
```Pascal
program eroare1;
var a,b:integer;
begin
    a:=2;
    b:=3+(a:=a+1);
end.
```

-	compileaza in limbajul original, dar nu si in MLP

```Pascal
program eroare2;
uses math; { MLP nu defineste unitati, nici comentarii >}
var a,b:integer;
begin
    a:=-5; b:=2;
    a:= min(a,b);
end.
```