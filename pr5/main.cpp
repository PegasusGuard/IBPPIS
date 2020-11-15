#include <iostream>
#include <windows.h>
#include <math.h>


using namespace std;

string encrypt (int key, string target);
string decrypt (int key, string target);

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);
    int choice = 1;
    int key;
    string target;
    while (choice != 0)
    {
        cout << "Какую операцию требуется произвести?" << endl
             << "1. Зашифровать" << endl
             << "2. Расшифровать" << endl
             << "3. Подобрать" << endl
             << "0. Покинуть программу :(" << endl
             << "Введите свой выбор здесь: ";
        cin >> choice;
        switch (choice)
        {
            case 0:
                break;
            case 1:
                cout << "Введите ключ: ";
                cin >> key;
                cout << "Введите сообщение: ";
                cin >> target;
                cout << encrypt(key,target) << endl;
                break;
            case 2:
                cout << "Введите ключ: ";
                cin >> key;
                cout << "Введите сообщение: ";
                cin >> target;
                cout << decrypt(key,target) << endl;
                break;
            case 3:
                cout << "Введите сообщение: ";
                cin >> target;
                for (int i = 1; i < target.size(); i++)
                cout << i << " " << decrypt(i, target) << endl;
                break;

        }
        cout << endl;
    }
    return 0;
}

string encrypt (int key, string target)
{
    string result;

        int counter = 0;
        for (int i = 0; i < key; i++)
        {
            for (int j = i; j <= target.size() - 1; j += key)
            {
                result += target[j];
            }
        }
    return result;
}
string decrypt (int key, string target)
{
    string result;
    int slice = floor((float)target.size()/(float)key) + 1;
    int addit = target.size() % key;
    int pos=0;
    string temp[key];
    for (int i = 0; i < key; i++)
    {
        if (addit == 0)
            slice--;
        temp[i] = target.substr(pos, slice);
        pos += slice;
        addit--;
    }
    slice++;
    for (int i = 0; i < slice; i++){
        for (int j = 0; j < key; j++)
        {
            if (i < temp[j].size())
                result += temp[j][i];
        }
    }
    return result;
}
