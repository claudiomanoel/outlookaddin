# coding: utf-8

# # Spam classification

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

PATH_MODELS ="./models/"

#load models
searchModelFileNames = PATH_MODELS + "model_*.pickle"
model_filenames = glob.glob( searchModelFileNames)
models_name = []
models = []

for i in range(len(model_filenames)):
    loaded_model = pickle.load(open(model_filenames[i], 'rb'))
    models_name.append(model_filenames[i].split("\\")[1].split(".")[0].replace("model_", ""))
    models.append(loaded_model)