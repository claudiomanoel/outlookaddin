import numpy as np # linear algebra
import pandas as pd # data processing, CSV file I/O (e.g. pd.read_csv)
import re
import glob, os

def readDataSet(pathRead):
    files = os.listdir(pathRead)
    files_txt = [i for i in files if i.endswith('.txt')]
    files_cats = [i for i in files if i.endswith('.cats')]
    data_txt = readFiles(pathRead, files_txt)
    data_cats = readFiles(pathRead, files_cats)
    return [data_cats, data_txt]


def readFiles(pathRead, files):
    data = []
    for file in files:
        df = open(pathRead + "/" + file).read()
        data.append(df)
    return data

# Input data files are available in the "../input/" directory.
# For example, running this (by clicking run or pressing Shift+Enter) will list the files in the input directory

dataset = readDataSet("enron_with_categories")

def remove_metadata(data):
    result = []
    pattern = re.compile('X-FileName: .*')
    pattern2 = re.compile('X-FileName: .*?  ')

    for doc in data:
        doc = doc.replace('\n', ' ')
        doc = doc.replace(' .*?nsf', '')
        match = pattern.search(doc).group(0)
        match = re.sub(pattern2, '', match)
        result.append(match)

    return result

email_bodies = dataset[1]
emails = remove_metadata(email_bodies[0:1700])

file = open("emails.txt", "w")

for email in emails:
    file.write(email + "\n")

file.close()

file = open("lab_emails.txt", "w")

for labeled in dataset[0][0:1700]:
    file.write(labeled + "***\n")

print('There are a total of {} emails\n'.format(len(emails)))
print('Sample email, unstructured content:\n\n', emails[1000]) 