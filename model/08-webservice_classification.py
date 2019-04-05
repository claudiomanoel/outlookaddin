import cherrypy
import json

# ## Import libraries

# In[5]:

import numpy as np
import pandas as pd
import time
import collections
import re
import random
import scipy.io
import glob

from sklearn.feature_extraction.text import CountVectorizer
from sklearn.feature_extraction.text import TfidfTransformer
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.svm import LinearSVC, SVC
from sklearn import preprocessing
from sklearn.metrics import accuracy_score, confusion_matrix
from sklearn import cross_validation, metrics   #Additional scklearn functions
from sklearn.grid_search import GridSearchCV   #Perforing grid search
from sklearn.feature_selection import SelectKBest
from sklearn.naive_bayes import BernoulliNB
from sklearn.ensemble import RandomForestClassifier
from sklearn.discriminant_analysis import LinearDiscriminantAnalysis
from sklearn.discriminant_analysis import QuadraticDiscriminantAnalysis
from sklearn.linear_model import LogisticRegression
from sklearn.tree import DecisionTreeClassifier
from sklearn.ensemble import AdaBoostClassifier
from sklearn import cross_validation, metrics   #Additional scklearn functions
from sklearn.grid_search import GridSearchCV
import pickle

from nltk.corpus import stopwords
from nltk.stem.wordnet import WordNetLemmatizer
import string

PATH_MODELS ="./models/"
EMAIL = "I love you so much, my little girl!"

#load models
searchModelFileNames = PATH_MODELS + "model_*.pickle"
model_filenames = glob.glob( searchModelFileNames)
models_name = []
models = []

for i in range(len(model_filenames)):
    loaded_model = pickle.load(open(model_filenames[i], 'rb'))
    models_name.append(model_filenames[i].split("\\")[1].split(".")[0].replace("model_", ""))
    models.append(loaded_model)

file = open("clean_data.txt", "r")
clean_data = file.readlines()
file.close()

vectorizer = TfidfVectorizer(stop_words='english', min_df=8)

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

def get_clean_data(data):
    words_to_exclude = set(stopwords.words('english'))
    exclude = set(string.punctuation)
    lemma = WordNetLemmatizer()  

    return [clean(doc, words_to_exclude, exclude, lemma) for doc in data]

class DataView(object):
    exposed = True
 
    @cherrypy.tools.accept(media='application/json')
 
    def POST(self):
        rawData = cherrypy.request.body.read(int(cherrypy.request.headers['Content-Length']))
        requestData = json.loads(rawData)
        requestId = requestData['id']
        email = requestData['email']
        normalized_data = get_clean_data([email])
        trainData = normalized_data + clean_data[1:1701]
        tfidEmail = vectorizer.fit_transform(trainData) 
        predicted_list = []

        for i in range(len(models)):
            predicted = models[i].predict(tfidEmail)[0]
            if predicted == 1:
                predicted_list.append(models_name[i])

        return json.dumps({'id': requestId, 'res': predicted_list})
 
def CORS():
    cherrypy.response.headers["Access-Control-Allow-Origin"] = "*"
    
 
if __name__ == '__main__':
    conf = {
        '/': {
            'request.dispatch': cherrypy.dispatch.MethodDispatcher(),
            'tools.CORS.on': True,
        }
    }
    cherrypy.tools.CORS = cherrypy.Tool('before_handler', CORS)
    cherrypy.quickstart(DataView(), '/email_classification/', conf)