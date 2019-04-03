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

# ## Vectorizer

# In[8]:

TOP_LEVEL_CATEGORY = "1"
SECOND_LEVEL_CATEGORY = "1"
FULL_LEVEL_CATEGORY = TOP_LEVEL_CATEGORY + "," + SECOND_LEVEL_CATEGORY + ","

file = open("clean_data.txt", "r")
clean_data = file.readlines()
file.close()

file = open("lab_emails.txt", "r")
label_data = file.read().split("***")
file.close()

def getData(clean_data, label_data):
    yes = []
    no = []
    for i in range(len(clean_data) - 1):
        label_line_data = label_data[i].split("\n")
        find_key = False
        for j in range(len(label_line_data)):
            if label_line_data[j].startswith(FULL_LEVEL_CATEGORY):
                find_key = True
                break
        if (find_key):
            yes.append(clean_data[i])
        else:
            no.append(clean_data[i])  
    return yes, no

yes_data, no_data = getData(clean_data, label_data)
all_data = yes_data + no_data

vectorizer = TfidfVectorizer(lowercase=True, stop_words="english", min_df=8) 
train_matrix = vectorizer.fit_transform(all_data)
X = train_matrix
Y = [1]*len(yes_data) + [0]*len(no_data)
number_of_samples = int(len(all_data) * 0.8)

# Save as .mat 
file_dict = {}
file_dict['training_data'] = X
file_dict['training_labels'] = Y
scipy.io.savemat('email_data.mat', file_dict)


# In[9]:

print(X.shape)


# ### # of features

# In[15]:

len(vectorizer.vocabulary_)


# ### feature rank

# In[ ]:

occ = np.asarray(train_matrix.sum(axis=0)).ravel().tolist()
counts_df = pd.DataFrame({'term': vectorizer.get_feature_names(), 'occurrences': occ})
counts_df.sort_values(by='occurrences', ascending=False).head(50)


# ##  Cross Validation

# In[ ]:

### SVM

def SVM_CV(data, n_samples, folds, C_hyperparam):
    #print "Problem 4: Spam Classification Cross Validation"
    
    X_raw = data['training_data'].toarray()
    y_raw = data['training_labels'].reshape(X_raw.shape[0],1)
    n = X_raw.shape[0]
    indices = np.arange(n)
    random.shuffle(indices)
    X_raw = X_raw[indices]
    y_raw = y_raw[indices]
    
    X = X_raw[:n_samples,:]
    y = y_raw[:n_samples]
    fold_size = int(len(y)/folds)
    
    best_result = -1
    best_hyperparam = -1

    for c in C_hyperparam:
        print("{:<6}{:<6}{:<8}{:<6}".format('C', 'Fold', 'Error', 'Time'))
        kfold_scores = []
        for i, x in enumerate(range(0, n_samples, fold_size)):
            start_time = time.time()
            all_idx  = set(range(0, n_samples))
            test_idx = set(range(x, x + fold_size))
            train_idx = sorted(list(all_idx - test_idx))
            test_idx  = sorted(list(test_idx))            

            train_fold_X, train_fold_y = X[train_idx], y[train_idx]
            test_fold_X,  test_fold_y  = X[test_idx],  y[test_idx]
            
            clf = LinearSVC(C=c).fit(train_fold_X, train_fold_y)
            predicted = clf.predict(test_fold_X)
            score = 1-accuracy_score(test_fold_y, predicted)
            kfold_scores.append(score)
            end_time = time.time()
            print ("{:<6}{:<6}{:<8}{:<6}".format(c, i, round(np.mean(score),4), round(end_time - start_time, 2)))

        mean_score = round(np.mean(kfold_scores), 4)
        mean_error = round(1 - mean_score, 4)

        if mean_error > best_result:
               best_result =  mean_error
               best_hyperparam = c
        print("Mean error: {}\nMean accuracy:    {}\n".format(mean_score, mean_error))
    best_model = LinearSVC(C=best_hyperparam).fit(X_raw, y_raw)
    return best_result, best_model


data = scipy.io.loadmat('./email_data.mat')
svm_best_result, svm_model = SVM_CV(data, n_samples=number_of_samples, folds=10, C_hyperparam=[.01, .1, 1, 10, 100])


# In[ ]:

### RANDOMFOREST

def randomforest_CV(data, n_samples, folds, C_hyperparam):
    #print "Problem 4: Spam Classification Cross Validation"
    
    X_raw = data['training_data'].toarray()
    y_raw = data['training_labels'].reshape(X_raw.shape[0],1)
    n = X_raw.shape[0]
    indices = np.arange(n)
    random.shuffle(indices)
    X_raw = X_raw[indices]
    y_raw = y_raw[indices]
    
    X = X_raw[:n_samples,:]
    y = y_raw[:n_samples]
    fold_size = int(len(y)/folds)
    
    best_result = -1
    best_hyperparam = -1

    for c in C_hyperparam:
        print ("{:<6}{:<6}{:<8}{:<6}".format('C', 'Fold', 'Error', 'Time'))
        kfold_scores = []
        for i, x in enumerate(range(0, n_samples, fold_size)):
            start_time = time.time()
            all_idx  = set(range(0, n_samples))
            test_idx = set(range(x, x + fold_size))
            train_idx = sorted(list(all_idx - test_idx))
            test_idx  = sorted(list(test_idx))            

            train_fold_X, train_fold_y = X[train_idx], y[train_idx]
            test_fold_X,  test_fold_y  = X[test_idx],  y[test_idx]
        
            # fit model no training data
            clf = RandomForestClassifier(n_estimators=c).fit(train_fold_X, train_fold_y)
            predicted = clf.predict(test_fold_X)
            score = 1-accuracy_score(test_fold_y, predicted)
            kfold_scores.append(score)
            end_time = time.time()
            print("{:<6}{:<6}{:<8}{:<6}".format(c, i, round(np.mean(score),4), round(end_time - start_time, 2)))

        mean_score = round(np.mean(kfold_scores), 4)
        mean_error = round(1 - mean_score, 4)
        if mean_error > best_result:
            best_result =  mean_error
            best_hyperparam = c
        print("Mean error: {}\nMean accuracy:    {}\n".format(mean_score, mean_error))
    best_model = RandomForestClassifier(n_estimators=best_hyperparam).fit(X_raw, y_raw)
    return best_result, best_model


data = scipy.io.loadmat('./email_data.mat')
random_forest_best_result, random_forest_model = randomforest_CV(data, n_samples=number_of_samples, folds=5, C_hyperparam=[1, 10, 100])

# In[3]:

### logreg

def logistic_CV(data, n_samples, folds, C_hyperparam):
    #print "Problem 4: Spam Classification Cross Validation"
    
    X_raw = data['training_data'].toarray()
    y_raw = data['training_labels'].reshape(X_raw.shape[0],1)
    n = X_raw.shape[0]
    indices = np.arange(n)
    random.shuffle(indices)
    X_raw = X_raw[indices]
    y_raw = y_raw[indices]
    
    X = X_raw[:n_samples,:]
    y = y_raw[:n_samples]
    fold_size = int(len(y)/folds)
    
    best_result = -1
    best_hyperparam = -1

    for c in C_hyperparam:
        print("{:<6}{:<6}{:<8}{:<6}".format('C', 'Fold', 'Error', 'Time'))
        kfold_scores = []
        for i, x in enumerate(range(0, n_samples, fold_size)):
            start_time = time.time()
            all_idx  = set(range(0, n_samples))
            test_idx = set(range(x, x + fold_size))
            train_idx = sorted(list(all_idx - test_idx))
            test_idx  = sorted(list(test_idx))            

            train_fold_X, train_fold_y = X[train_idx], y[train_idx]
            test_fold_X,  test_fold_y  = X[test_idx],  y[test_idx]

            # fit model no training data
            clf =  LogisticRegression(C=c).fit(train_fold_X, train_fold_y)
            predicted = clf.predict(test_fold_X)
            score = 1-accuracy_score(test_fold_y, predicted)
            kfold_scores.append(score)
            end_time = time.time()
            print("{:<6}{:<6}{:<8}{:<6}".format(c, i, round(np.mean(score),4), round(end_time - start_time, 2)))

        mean_score = round(np.mean(kfold_scores), 4)
        mean_error = round(1 - mean_score, 4)
        if mean_error > best_result:
            best_result =  mean_error
            best_hyperparam = c
        print("Mean error: {}\nMean accuracy:    {}\n".format(mean_score, mean_error))
    best_model = LogisticRegression(C=best_hyperparam).fit(X_raw, y_raw)
    return best_result, best_model


data = scipy.io.loadmat('./email_data.mat')
logistic_best_result, logistic_model = logistic_CV(data, n_samples=number_of_samples, folds=5, C_hyperparam=[0.001, 0.01, 0.1, 1, 10, 100])

#Find best result
best_results = []
best_results.append(svm_best_result)
best_results.append(random_forest_best_result)
best_results.append(logistic_best_result)
best_result = max(best_results)
best_model = svm_model

if (best_result == svm_best_result):
    print ("SVM Wins")
    best_model = svm_model
if (best_result == random_forest_best_result):
    print ("Random Forest Wins")
    best_model = random_forest_model
if (best_result == logistic_best_result):
    print ("Logistic Wins")
    best_model = logistic_model

print("Best accuracy: {}".format(best_result))
model_file_name =  "./models/model_" + TOP_LEVEL_CATEGORY + "_" + SECOND_LEVEL_CATEGORY + ".pickle"
pickle.dump(best_model, open(model_file_name, "wb"))