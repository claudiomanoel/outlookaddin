from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import linear_kernel

file = open("clean_data.txt", "r")
clean_data = file.readlines()
file.close()

vectorizer = TfidfVectorizer(stop_words='english', max_df=0.50, min_df=2)

trainData = clean_data[0:1000]
X = vectorizer.fit_transform(trainData)

cosine_sim = linear_kernel(X[0:1], X).flatten()
print(cosine_sim)

query = 'salary'

vec_query = vectorizer.transform([query])
cosine_sim = linear_kernel(vec_query, X).flatten()
related_email_indices = cosine_sim.argsort()[:-10:-1]
print(related_email_indices)

first_email_index = related_email_indices[0]
print(trainData[first_email_index])