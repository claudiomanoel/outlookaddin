from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.decomposition import PCA
from sklearn.cluster import KMeans
from sklearn.metrics.pairwise import linear_kernel
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

def top_tfidf_feats(row, features, top_n=20):
    topn_ids = np.argsort(row)[::-1][:top_n]
    top_feats = [(features[i], row[i]) for i in topn_ids]
    df = pd.DataFrame(top_feats, columns=['features', 'score'])
    return df
def top_feats_in_doc(X, features, row_id, top_n=25):
    row = np.squeeze(X[row_id].toarray())
    return top_tfidf_feats(row, features, top_n)
    
file = open("clean_data.txt", "r")
clean_data = file.readlines()
file.close()

vectorizer = TfidfVectorizer(stop_words='english', max_df=0.50, min_df=2)

trainData = clean_data[0:1000]
X = vectorizer.fit_transform(trainData)

features = vectorizer.get_feature_names()
print (top_feats_in_doc(X, features, 1, 10))