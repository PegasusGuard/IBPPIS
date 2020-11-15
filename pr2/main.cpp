#include <iostream>
#include <windows.h>
#include <string>

using namespace std;

void encrypt(char alpha[5][7], string target);
void decrypt(char alpha[5][7], string target);
void soften(string &target);
int findi(char alpha[5][7], char target);
int findj(char alpha[5][7], char target);

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    string tempalpha="АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЩЬЫЭЮЯ";
    string key;
    string target;
    int cypher;
    cout << "Введите фразу для шифрования/дешифрования без пробелов: ";
    cin >> target;
    cout << "Введите ключ шифрования: ";
    cin >> key;
    cout << "Чтобы произвести шифрование, нажмите 1\nЧтобы произвести дешифрование, нажмите 2 ";
    cin >> cypher;
    soften(target);
    for (char& c : key)
        {
        tempalpha.erase(tempalpha.find(c),1);
        }
    char alpha[5][7];
    string temp = key + tempalpha;
    int flag=0;
    for (int i = 0;i < 5;i++)
    {
        for (int j=0; j < 6; j++)
        {
            alpha[i][j] = temp.at(flag);
            flag++;
        }
    }
    for (int i = 0;i < 5;i++)
    {
        for (int j=0; j < 6; j++)
        {
            cout << alpha[i][j] << ' ';
        }
        cout << endl;
    }
    if (cypher == 1)
        encrypt(alpha, target);
    if (cypher == 2)
        decrypt(alpha, target);
    return 0;
}

void soften(string &target)
{
    while (target.find('Ъ') != string::npos)
        target.replace(target.find('Ъ'),1,1,'Ь');
    if (target.size()%2 != 0)
        target.append("Х");

}

int findi(char alpha[5][7], char target)
{
    for (int i = 0;i < 5;i++)
    {
        for (int j=0; j < 6; j++)
        {
            if (alpha[i][j] == target)
                return i;
        }
    }
    return 0;
}

int findj(char alpha[5][7], char target)
{
    for (int i = 0;i < 5;i++)
    {
        for (int j=0; j < 6; j++)
        {
            if (alpha[i][j] == target)
                return j;
        }
    }
    return 0;
}

void encrypt(char alpha[5][7], string target)
{
    string goal;
    int i = 0;
    while (i < target.size())
    {
        char letter1 = target.at(i);
        char letter2 = target.at(i+1);
        int posi1 = findi(alpha, letter1);
        int posi2 = findi(alpha, letter2);
        int posj1 = findj(alpha, letter1);
        int posj2 = findj(alpha, letter2);
        if (letter1 == letter2)
            target.insert(i+1, "Х");
        else if (posi1 == posi2)
        {
            if (posj1 == 5)
                goal.push_back(alpha[posi1][0]);
            else
                goal.push_back(alpha[posi1][posj1+1]);
            if (posj2 == 5)
                goal.push_back(alpha[posi2][0]);
            else
                goal.push_back(alpha[posi2][posj2+1]);
            i+=2;
        }
        else if (posj1 == posj2)
        {
            if (posi1 == 4)
                goal.push_back(alpha[0][posj1]);
            else
                goal.push_back(alpha[posi1+1][posj1]);
            if (posi2 == 4)
                goal.push_back(alpha[0][posj2]);
            else
                goal.push_back(alpha[posi2+1][posj2]);
            i+=2;
        }
        else
        {
            goal.push_back(alpha[posi1][posj2]);
            goal.push_back(alpha[posi2][posj1]);
            i+=2;
        }
    }
    cout << goal << endl;
}

void decrypt(char alpha[5][7], string target)
{
    string goal;
    int i = 0;
    while (i < target.size())
    {
        char letter1 = target.at(i);
        char letter2 = target.at(i+1);
        int posi1 = findi(alpha, letter1);
        int posi2 = findi(alpha, letter2);
        int posj1 = findj(alpha, letter1);
        int posj2 = findj(alpha, letter2);
        if (posi1 == posi2)
        {
            if (posj1 == 0)
                goal.push_back(alpha[posi1][5]);
            else
                goal.push_back(alpha[posi1][posj1-1]);
            if (posj2 == 0)
                goal.push_back(alpha[posi2][5]);
            else
                goal.push_back(alpha[posi2][posj2-1]);
            i+=2;
        }
        else if (posj1 == posj2)
        {
            if (posi1 == 0)
                goal.push_back(alpha[4][posj1]);
            else
                goal.push_back(alpha[posi1-1][posj1]);
            if (posi2 == 0)
                goal.push_back(alpha[4][posj2]);
            else
                goal.push_back(alpha[posi2-1][posj2]);
            i+=2;
        }
        else
        {
            goal.push_back(alpha[posi1][posj2]);
            goal.push_back(alpha[posi2][posj1]);
            i+=2;
        }
    }
    cout << goal << endl;
}
