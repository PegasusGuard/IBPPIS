#include <iostream>
#include <windows.h>
#include <string>
#include <cctype>
#include <algorithm>


using namespace std;

string encryptindex (char alpha[6][7], string target);
string encryptshift (char alpha[6][7], string target);
string decryptindex (char alpha[6][7], string target);
string decryptshift (char alpha[6][7], string target);
int findi(char alpha[6][7], char target);
int findj(char alpha[6][7], char target);

int charToInt(char c)
{
   int arr[]={0,1,2,3,4,5,6,7,8,9};
   return arr[c-'0'];
}

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    string target;
    int flag = 0;
    int action = 1;
    char alpha[6][7];
    for (int i = 0;i < 6;i++)
    {
        for (int j=0; j < 6; j++)
        {
            if (flag < 33)
                alpha[i][j] = alphabet.at(flag);
            flag++;
        }
    }
    alpha[5][5]=' ';

    while (action != 0)
    {
        cout << "Введите строку(последовательность букв или пар цифр): ";
        cin >> target;
        cout << "Что требуется сделать со строкой?" << endl
             << "0. Завершить работу" << endl
             << "1. Зашифровать (индексы)" << endl
             << "2. Зашифровать (сдвиг)" << endl
             << "3. Зашифровать (комбо)" << endl
             << "4. Расшифровать (индексы)" << endl
             << "5. Расшифровать (сдвиг)" << endl
             << "6. Расшифровать (комбо)" << endl
             << "Введите номер действия: ";
        cin >> action;
        switch (action)
        {
            case 0:
                break;
            case 1:
                cout << encryptindex(alpha, target) << endl;
                break;
            case 2:
                cout << encryptshift(alpha, target) << endl;
                break;
            case 3:
                cout << encryptindex(alpha, encryptshift(alpha, target)) << endl;
                break;
            case 4:
                cout << decryptindex(alpha, target) << endl;
                break;
            case 5:
                cout << decryptshift(alpha, target) << endl;
                break;
            case 6:
                cout << decryptshift(alpha, decryptindex(alpha, target)) << endl;
                break;
            default:
                cout << "Неправильный ввод, может быть получится в следующий раз" << endl;
                break;
        }
        cout << endl;
    }
    return 0;
}

int findi(char alpha[6][7], char target)
{
    for (int i = 0;i < 6;i++)
    {
        for (int j=0; j < 6; j++)
        {
            if (alpha[i][j] == target)
                return i;
        }
    }
    return 0;
}

int findj(char alpha[6][7], char target)
{
    for (int i = 0;i < 6;i++)
    {
        for (int j=0; j < 6; j++)
        {
            if (alpha[i][j] == target)
                return j;
        }
    }
    return 0;
}

string encryptindex (char alpha[6][7], string target)
{
    string result;
    for (char& c : target)
    {
        result.append(to_string(findi(alpha, c) + 1));
        result.append(to_string(findj(alpha, c) + 1));
        result.append(" ");
    }
    return result;
}

string encryptshift (char alpha[6][7], string target)
{
    string result;
    for (char& c : target)
    {
        if ((findi(alpha, c) == 5) || ((findi(alpha, c) == 4) && (findj(alpha, c) > 2)))
            result += alpha[0][findj(alpha,c)];
        else
            result += alpha[findi(alpha, c) + 1][findj(alpha,c)];
    }
    return result;
}

string decryptindex (char alpha[6][7], string target)
{
    string result;
    target.erase(
                 remove_if(target.begin(),target.end(), static_cast<int(*)(int)>(isspace)),target.end());
    while (target.size() > 1)
    {
        int iPos = charToInt(target.at(0)) - 1;
        int jPos = charToInt(target.at(1)) - 1;
        result += alpha[iPos][jPos];
        target.erase(0, 2);
    }
    return result;
}

string decryptshift (char alpha[6][7], string target)
{
    string result;
    for (char& c : target)
    {
        if (findi(alpha, c) == 0)
            {
            if(findj(alpha, c) < 3)
                result += alpha[5][findj(alpha, c)];
            else
                result += alpha[4][findj(alpha, c)];
            }
        else
            result += alpha[findi(alpha, c) - 1][findj(alpha,c)];

    }
    return result;
}
