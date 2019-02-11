from nltk.corpus import stopwords
from nltk.stem.wordnet import WordNetLemmatizer
import string

def is_number(s):
    try:
        float(s)
        return True
    except ValueError:
        pass
 
    try:
        import unicodedata
        unicodedata.numeric(s)
        return True
    except (TypeError, ValueError):
        pass
 
    return False

def clean(doc, words_to_exclude, exclude, lemma):
    word_free = " ".join([i for i in doc.lower().split() if i not in words_to_exclude])
    punc_free = ''.join(ch for ch in word_free if ch not in exclude)
    number_free = " ".join([i for i in punc_free.split() if is_number(i) == False])
    normalized = " ".join(lemma.lemmatize(word) for word in number_free.split())

    return normalized

def clean_data(data):
    words_to_exclude = set(stopwords.words('english'))
    exclude = set(string.punctuation)
    lemma = WordNetLemmatizer()  

    return [clean(doc, words_to_exclude, exclude, lemma) for doc in data]

file = open("emails.txt", "r")
dataset = file.readlines()
file.close()

normalized_data = clean_data(dataset)

file = open("clean_data.txt", "w")

for clean_email in normalized_data:
    file.write(clean_email + "\n")

file.close()