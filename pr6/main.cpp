#include <iostream>
#include <algorithm>
#include <windows.h>
#include <math.h>
#include <array>

using namespace std;

void swap(char *str1, char *str2);
int countX(string s);
string decypher(string target, string key);

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    cout << "¬ведите сообщение: ";
    cin >> target;
    cout << "¬ведите часть ключа: ";
    cin >> keypart;
    string secondpart;


    int secondsize = countX(keypart);

    for (int i = 1; i <= keypart.size(); i++)
    {
        if (keypart.find(to_string(i)) == keypart.npos)

            secondpart += to_string(i);
    }


    do {
        int flag = 0;
        string key = keypart;
        for (int i = 0; i < key.size(); i++)
        {
            if (key[i] == 'X')
            {
                key.insert(i, 1, secondpart[flag]);
                key.erase(i+1, 1);
                flag++;
            }
        }
        cout << decypher(target, key) << "| ключ :" << key << endl;
    } while(next_permutation(secondpart.begin(), secondpart.end()));




    return 0;
}

void swap(string *str1, string *str2)
{
  string *temp = str1;
  str1 = str2;
  str2 = temp;
}

int countX(string s) {
  int count = 0;

  for (int i = 0; i < s.size(); i++)
    if (s[i] == 'X') count++;

  return count;
}

string decypher(string target, string key)
{
    string result;
    int slice = ceil((float)target.size()/(float)key.size());
    string matrix[slice];
    for (int i = 0; i < slice; i++)
    {
        matrix[i] = target.substr(i * key.size(), key.size());

    }
    for (int i = 0; i < slice; i++)
    {
        for(char& c : key)
        {
            int a = c - 49;
            result += matrix[a][i];
        }
    }
    return result;
}
